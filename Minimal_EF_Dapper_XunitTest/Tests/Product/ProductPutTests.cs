using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minimal_EF_Dapper.Endpoints.Segmented.Products;

namespace Minimal_EF_Dapper_XunitTest
{
    public class ProductPutTests
    {
        //[Fact]
        //public void Action_ShouldReturnNotFoundResult_WhenProductIsNotFound()
        //{
        //    // Arrange
        //    var dbContext = GetMockedDbContext();
        //    dbContext.Setup(x => x.Products.FirstOrDefault(It.IsAny<Func<Product, bool>>()))
        //        .Returns((Product)null);
        //    var httpContext = new DefaultHttpContext();

        //    // Act
        //    var result = ProductPut.Action(Guid.NewGuid(), new ProductRequestDTO(), httpContext, dbContext.Object);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public void Action_ShouldReturnNotFoundResult_WhenCategoryIsNotFound()
        //{
        //    // Arrange
        //    var dbContext = GetMockedDbContext();
        //    dbContext.Setup(x => x.Products.FirstOrDefault(It.IsAny<Func<Product, bool>>()))
        //        .Returns(new Product());
        //    dbContext.Setup(x => x.Categories.FirstOrDefault(It.IsAny<Func<Category, bool>>()))
        //        .Returns((Category)null);
        //    var httpContext = new DefaultHttpContext();

        //    // Act
        //    var result = ProductPut.Action(Guid.NewGuid(), new ProductRequestDTO(), httpContext, dbContext.Object);

        //    // Assert
        //    Assert.IsType<NotFoundResult>(result);
        //}

        //[Fact]
        //public void Action_ShouldReturnValidationProblemResult_WhenProductIsNotValid()
        //{
        //    // Arrange
        //    var dbContext = GetMockedDbContext();
        //    var product = new Product();
        //    product.AddProduct("product name", "product description", 10, true, new Category(), "user");
        //    product.EditProduct("", 0, true, new Category(), "user");
        //    dbContext.Setup(x => x.Products.FirstOrDefault(It.IsAny<Func<Product, bool>>()))
        //        .Returns(product);
        //    dbContext.Setup(x => x.Categories.FirstOrDefault(It.IsAny<Func<Category, bool>>()))
        //        .Returns(new Category());
        //    var httpContext = new DefaultHttpContext();

        //    // Act
        //    var result = ProductPut.Action(Guid.NewGuid(), new ProductRequestDTO(), httpContext, dbContext.Object);

        //    // Assert
        //    Assert.IsType<ValidationProblemResult>(result);
        }
    }