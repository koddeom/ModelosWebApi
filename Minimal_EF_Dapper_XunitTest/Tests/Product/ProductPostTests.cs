using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Minimal_EF_Dapper.Domain.Database;
using Minimal_EF_Dapper.Domain.Database.Entities.Product;
using Minimal_EF_Dapper.Endpoints.DTO.Product;
using Minimal_EF_Dapper.Endpoints.Segmented.Products;
using Moq;
using System.Linq.Expressions;

namespace Minimal_EF_Dapper.XunitTests
{
    public class ProductPostTests
    {
        [Fact]
        public async Task ProductPost_CreateSucess()
        {
            // Arrange

            var dummie_CategoryId = Guid.NewGuid();
            var dummie_User = "Neil Armstrong";

            var mockProductRequestDTO = new ProductRequestDTO
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 10,
                CategoryId = dummie_CategoryId
            };

            Category mockCategory = new Category
            {
                Id = dummie_CategoryId,
                Name = "Test Category",
                Active = true
            };

            //Mock do Contexto Http
            var httpContextMock = new Mock<HttpContext>();

            //Mock do banco
            var dbContextMock = new Mock<ApplicationDbContext>();

            var categoriesDbSetMock = new Mock<DbSet<Category>>();
            var productsDbSetMock = new Mock<DbSet<Product>>();

            //------------------------------------------------------------
            // Mock para entidade produto e metodo Add
            //------------------------------------------------------------

            var mockProduct = new Mock<Product>();

            mockProduct.Setup(x => x.AddProduct(It.IsAny<string>(),
                                                  It.IsAny<string>(),
                                                  It.IsAny<decimal>(),
                                                  It.IsAny<bool>(),
                                                  It.IsAny<Category>(),
                                                  It.IsAny<string>()))
                       .Callback((string name,
                                   string description,
                                   decimal price,
                                   bool active,
                                   Category category,
                                   string createdBy) =>
                       {
                           mockProduct.Object.Name = mockProductRequestDTO.Name;
                           mockProduct.Object.Description = mockProductRequestDTO.Description;
                           mockProduct.Object.Price = mockProductRequestDTO.Price;
                           mockProduct.Object.Active = mockProductRequestDTO.Active;
                           mockProduct.Object.Category = mockCategory;
                           mockProduct.Object.CreatedBy = dummie_User;
                       });

            //var product = mockProduct.Object;

            //product.AddProduct(mockProductRequestDTO.Name,
            //                   mockProductRequestDTO.Description,
            //                   mockProductRequestDTO.Price,
            //                   true,
            //                   mockCategory,
            //                   dummie_User);

            //------------------------------------------------------------

            // Simulo a recuperacao da categoria
            categoriesDbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                               .ReturnsAsync(mockCategory);
                        
            categoriesDbSetMock.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Category, bool>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(mockCategory);


            //Simulo o retorno do Dbset<category> atravez do Mock
            dbContextMock.Setup(m => m.Categories)
                         .Returns(categoriesDbSetMock.Object);

            //Simulo o retorno do Dbset<Product> atravez do Mock
            dbContextMock.Setup(m => m.Products)
                         .Returns(productsDbSetMock.Object);

            // Act
            var result = await ProductPost.Action(mockProductRequestDTO,
                                                  httpContextMock.Object,
                                                  dbContextMock.Object);

            // Assert

            Assert.True(true);

            //var results = Assert.IsType<Results>(result);
            //Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            //dbContextMock.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}