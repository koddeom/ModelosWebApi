using AutoMapper;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.UnitOfWork.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Controllers;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductController> _logger;

    public ProductControllerTests(WebApplicationFactory<Program> factory,
                                  IMapper mapper,
                                  IUnitOfWork unitOfWork,
                                  ILogger<ProductController> logger)
    {
        _factory = factory;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [Fact]
    public async Task Post_ShouldReturnOkResult()
    {
        // Arrange
        var client = _factory.CreateClient();

        var productRequestDTO = new ProductRequestDTO()
        {
            Name = "Test Product",
            Description = "Test Product Description",
            Price = 10,
            CategoryId = Guid.NewGuid(),
        };

        var controller = new ProductController(_logger, _mapper, _unitOfWork);

        // Act
        var result = await controller.ProductPost(productRequestDTO);

        // Assert
        Assert.Inconclusive();
    }

    //[Fact]
    //public async Task Put_ShouldReturnOkResult()
    //{
    //    // Arrange
    //    var client = _factory.CreateClient();
    //    var product = new ProductViewModel
    //    {
    //        Id = 1,
    //        Name = "Test Product",
    //        Description = "Test Description",
    //        Price = 99.99m
    //    };
    //    var controller = new ProductController(_unitOfWork, _mapper, _logger);

    //    // Act
    //    var result = await controller.Put(product.Id, product);

    //    // Assert
    //    Assert.IsType<OkResult>(result);
    //}

    //[Fact]
    //public async Task Delete_ShouldReturnOkResult()
    //{
    //    // Arrange
    //    var client = _factory.CreateClient();
    //    var controller = new ProductController(_unitOfWork, _mapper, _logger);

    //    // Act
    //    var result = await controller.Delete(1);

    //    // Assert
    //    Assert.IsType<OkResult>(result);
    //}
}