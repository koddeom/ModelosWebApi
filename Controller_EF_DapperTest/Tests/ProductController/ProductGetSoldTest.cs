using Controller_EF_Dapper.Business;
using Controller_EF_Dapper.Endpoints.Products.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Controller_EF_DapperTest.Tests.ProductController
{
    [TestClass]
    public class ProductGetSoldTest
    {
        //[TestMethod]
        //public void ProductSoldGet_ShouldReturnOkResult()
        //{
        //    // Arrange
        //    var mockLogger = new Mock<ILogger<ProductController>>();
        //    var mockService = new Mock<ServiceAllProductsSold>();
        //    mockService.Setup(s => s.Execute()).Returns(new ProductSoldResponseDTO());

        //    var controller = new ProductController(mockLogger.Object, null, mockService.Object);

        //    // Act
        //    var result = controller.ProductSoldGet();

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        //}
    }
}
