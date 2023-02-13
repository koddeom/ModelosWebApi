using Microsoft.AspNetCore.Mvc;
using Minimal_EF_Dapper.Endpoints.Segmented.Products;

namespace Minimal_EF_Dapper_XunitTest
{
        public class ProductDeleteTests
        {
            //[Fact]
            //public void When_Deleting_A_Product_That_Does_Not_Exist_Should_Return_NotFound()
            //{
            //    Arrange
            //   var id = Guid.NewGuid();
            //    var dbContextMock = new Mock<ApplicationDbContext>();
            //    dbContextMock.Setup(x => x.Products.Find(id)).Returns((Product)null);

            //    Act
            //   var result = ProductDelete.Action(id, dbContextMock.Object);

            //    Assert
            //    Assert.IsType<NotFoundResult>(result);
            //}

            //[Fact]
            //public void When_Deleting_A_Product_Should_Return_Ok()
            //{
            //    Arrange
            //   var id = Guid.NewGuid();
            //    var product = new Product { Id = id };

            //    var dbContextMock = new Mock<ApplicationDbContext>();
            //    dbContextMock.Setup(x => x.Products.Find(id)).Returns(product);

            //    Act
            //   var result = ProductDelete.Action(id, dbContextMock.Object);

            //    Assert
            //    Assert.IsType<OkResult>(result);
            //    dbContextMock.Verify(x => x.Products.Remove(product), Times.Once());
            //    dbContextMock.Verify(x => x.SaveChanges(), Times.Once());
            //}
    }
}