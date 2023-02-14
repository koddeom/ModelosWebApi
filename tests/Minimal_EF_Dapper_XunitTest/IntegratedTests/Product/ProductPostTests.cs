using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minimal_EF_Dapper.Domain.Database;
using Minimal_EF_Dapper.Domain.Database.Entities.Product;
using Minimal_EF_Dapper.Endpoints.DTO.Product;
using Minimal_EF_Dapper.Endpoints.Segmented.Products;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Minimal_EF_Dapper_XunitTest
{
    //==================================================================================================
    // Caso especial
    // O Mock de objetos com retorno IQueryable � complicado pela falta de integra��io
    // das Libs de Mock com o Core.EF
    // A Solucao passa pelo uso de duas libs:
    //  - NSubstitute
    //  - MockQueryable.NSubstitute
    //==================================================================================================
    // A linha problematica para o teste foi:
    //
    //    var category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == productRequestDTO.CategoryId);
    //
    //  Na verdade, a linha em quest�o � mocada implicitamente pelo MockableQuery criado por MockQueryable.NSubstitute.
    //  O que ocorre � que, no m�todo Action, a express�o "c => c.Id == productRequestDTO.CategoryId"
    //  que � passada para o m�todo "FirstOrDefaultAsync' � uma extens�o do tipo IQueryable do Entity Framework.
    //
    // O pulo do gato:
    //
    // Como o dbContext.Categories � um DbSet<Category>, ele implementa IQueryable<Category>
    // e, portanto, pode ser "mockado" pelo MockableQuery.
    //
    // Assim:
    //
    //  Ao utilizar o m�todo GetQueryableMock do MockableQuery
    //  � criado um objeto que implementa IQueryable<Category>
    //  e que pode ser usado no lugar do dbContext.Categories.
    //
    // Dessa forma, quando a express�o c => c.Id == productRequestDTO.CategoryId
    // � passada para o m�todo FirstOrDefaultAsync do MockableQuery
    // ela � aplicada a essa inst�ncia em mem�ria do DbSet<Category>,
    // em vez de ser enviada ao banco de dados.
    //
    // Por fim, o MockableQuery retorna o resultado esperado para
    // o m�todo FirstOrDefaultAsync de acordo
    // com o argumento da express�o passado pelo teste.
    //==================================================================================================
    // Sim esse coment�rio � grande porque foi foda achar uma solu��o para esse problema, ent�o...
    // PAU NO SEU CU UNCLE BOB
    //==================================================================================================

    public class ProductPostTests
    {
        [Fact]
        public async Task ProductPost_CreatedWithSucess()
        {
            // Arrange --------------------------------------------------------------------------------------------------

            // Mock dos contextos
            var dbContextMock = Substitute.For<ApplicationDbContext>();
            var httpContextMock = Substitute.For<HttpContext>();

            // Mock dos dados
            var dummie_CategoryId = Guid.NewGuid();

            var mockProductRequestDTO = new ProductRequestDTO
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10,
                CategoryId = dummie_CategoryId
            };

            var mockCategory = new Category
            {
                Id = dummie_CategoryId,
                Name = "Test Category",
                Active = true
            };

            //Preparando o DBSet para interagir com metodos IQueryable

            //1 - Crio uma lista com os dados mockados
            var mockCategories = new List<Category> { mockCategory };

            //2- Transformo a lista em um tipo queryable
            var mockCategoriesQueryable = mockCategories.AsQueryable().BuildMockDbSet();

            //3- Digo qual sera o retorno do retorno do DbSet<Category>
            dbContextMock.Categories.Returns(mockCategoriesQueryable);

            // Act ----------------------------------------------------------------------------------------------------
            var result = await ProductPost.Action(mockProductRequestDTO, httpContextMock, dbContextMock);

            var objectResponse = (ObjectResult)result;

            // Assert
            var ObjectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status201Created, ObjectResult.StatusCode);
        }

        [Fact]
        public async Task ProductPost_ReturnError_PriceZero()
        {
            // Arrange
            var dbContextMock = Substitute.For<ApplicationDbContext>();
            var httpContextMock = Substitute.For<HttpContext>();

            var dummie_CategoryId = Guid.NewGuid();
            var mockProductRequestDTO = new ProductRequestDTO
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 0,
                CategoryId = dummie_CategoryId
            };

            var mockCategory = new Category
            {
                Id = dummie_CategoryId,
                Name = "Test Category",
                Active = true
            };

            // Mockando o DbSet<Category> com MockQueryable.NSubstitute
            var categoryList = new List<Category> { mockCategory };
            var categoryQueryable = categoryList.AsQueryable().BuildMockDbSet();
            dbContextMock.Categories.Returns(categoryQueryable);

            // Act
            var result = await ProductPost.Action(mockProductRequestDTO, httpContextMock, dbContextMock);

            // Assert
            var ObjectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, ObjectResult.StatusCode);
        }
    }
}