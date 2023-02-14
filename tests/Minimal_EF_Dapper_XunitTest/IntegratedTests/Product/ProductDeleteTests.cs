using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minimal_EF_Dapper.Domain.Database;
using Minimal_EF_Dapper.Domain.Database.Entities.Product;
using Minimal_EF_Dapper.Endpoints.Segmented.Products;
using MockQueryable.NSubstitute;
using NSubstitute;

namespace Minimal_EF_Dapper_XunitTest
{
    public class ProductDeleteTests
    {
        [Fact]
        public void ProductDelete_DeletedWithSucess()
        {
            // Arrange

            // Contextos
            var httpContextMock = Substitute.For<HttpContext>();
            var dbContextMock = Substitute.For<ApplicationDbContext>();

            //Dados
            var dummie_ProductId = Guid.NewGuid();
            var dummie_CategoryId = Guid.NewGuid();
            var dummie_user = "doe joe";

            var mockCategory = new Category
            {
                Id = dummie_CategoryId,
                Name = "Test Category",
                Active = true
            };

            var mockProduct = new Product
            {
                Id = dummie_ProductId,
                Category = mockCategory,
                Name = "Test Product",
                Description = "Test Description",
                Price = 10,
                EditedBy = dummie_user,
                EditedOn = DateTime.Now
            };

            // Configurando as tabelas vituais

            //1 - Crio uma lista com os dados mockados
            var mockCategories = new List<Category> { mockCategory };
            var mockProducts = new List<Product> { mockProduct };

            //2- Transformo a lista em um tipo queryable
            var mockCategoriesQueryable = mockCategories.AsQueryable().BuildMockDbSet();
            var mockProductsQueryable = mockProducts.AsQueryable().BuildMockDbSet();

            //3- Digo qual sera o retorno do retorno do DbSet<Category>
            dbContextMock.Products.Returns(mockProductsQueryable);
            dbContextMock.Categories.Returns(mockCategoriesQueryable);

            // Act
            var result = ProductDelete.Action(dummie_ProductId, dbContextMock);
            // Assert
            var ObjectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, ObjectResult.StatusCode);
        }

        [Fact]
        public void ProductDelete_NotFound()
        {
            // Arrange

            // Contextos
            var httpContextMock = Substitute.For<HttpContext>();
            var dbContextMock = Substitute.For<ApplicationDbContext>();

            //Dados
            var dummie_ProductId = Guid.NewGuid();
            var dummie_CategoryId = Guid.NewGuid();
            var dummie_user = "doe joe";

            var mockCategory = new Category
            {
                Id = dummie_CategoryId,
                Name = "Test Category",
                Active = true
            };

            var mockProduct = new Product
            {
                Id = dummie_ProductId,
                Category = mockCategory,
                Name = "Test Product",
                Description = "Test Description",
                Price = 10,
                EditedBy = dummie_user,
                EditedOn = DateTime.Now
            };

            // Configurando as tabelas vituais

            //1 - Crio uma lista com os dados mockados
            var mockCategories = new List<Category> { mockCategory };
            var mockProducts = new List<Product> { }; //==> NotFound

            //2- Transformo a lista em um tipo queryable
            var mockCategoriesQueryable = mockCategories.AsQueryable().BuildMockDbSet();
            var mockProductsQueryable = mockProducts.AsQueryable().BuildMockDbSet();

            //3- Digo qual sera o retorno do retorno do DbSet<Category>
            dbContextMock.Products.Returns(mockProductsQueryable);
            dbContextMock.Categories.Returns(mockCategoriesQueryable);

            // Act
            var result = ProductDelete.Action(dummie_ProductId, dbContextMock);
            // Assert
            var ObjectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, ObjectResult.StatusCode);
        }
    }
}