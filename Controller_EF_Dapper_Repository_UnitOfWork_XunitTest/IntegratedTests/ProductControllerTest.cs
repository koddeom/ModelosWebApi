using AutoMapper;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.Database.Entities;
using Controller_EF_Dapper_Repository_UnityOfWork.AppDomain.UnitOfWork.Interface;
using Controller_EF_Dapper_Repository_UnityOfWork.Controllers;
using Controller_EF_Dapper_Repository_UnityOfWork.Endpoints.Products.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Controller_EF_Dapper_Repository_UnitOfWork_XunitTest
{
    public class ProductControllerTests
    {
        private readonly ProductController _productController;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public ProductControllerTests()
        {
            _loggerMock = new Mock<ILogger<ProductController>>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _productController = new ProductController(_loggerMock.Object, _mapperMock.Object, _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task ProductPost_CreatedWithSucess()
        {
            // Arrange

            // Crio um Dummie para categoria
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category Dumie"
            };

            // Crio um Dummie de input dos dados
            var productRequestDTO = new ProductRequestDTO
            {
                Name = "Product Dumie",
                Description = "Description for product Dumie",
                Price = 1500,
                Active = true,
                CategoryId = category.Id,
            };

            // Crio um Dummie para a saida Esperada de Product
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Category = category,
                Name = productRequestDTO.Name,
                Description = productRequestDTO.Description,
                Price = productRequestDTO.Price,
                CreatedBy = "doe joe",
                CreatedOn = DateTime.Now
            };

            // Crio comportamentos virtuais para as dependencias
            // utilizadas no EndPoint ProductPost
            // Eles vão enganar o endpoint no teste integrado

            //UnitOfWork ------------------------------------------------------------------
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(x => x.Categories.Get(It.IsAny<Guid>()))
                                                  .ReturnsAsync(category);

            unitOfWorkMock.Setup(x => x.Products.Add(It.IsAny<Product>()))
                                                .Callback<Product>(p => p.Id = product.Id);

            //Mapper ------------------------------------------------------------------
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductRequestDTO>()))
                                    .Returns(product);

            mapperMock.Setup(x => x.Map<ProductResponseDTO>(It.IsAny<Product>()))
                                   .Returns(new ProductResponseDTO { Id = product.Id });

            //Logger ------------------------------------------------------------------
            var loggerMock = new Mock<ILogger<ProductController>>();

            //Controller------------------------------------------------------------------
            var productController = new ProductController(loggerMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await productController.ProductPost(productRequestDTO);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);

            //Obtendo o valor de "Location" Por reflexao
            var locationProperty = createdResult.Value.GetType().GetProperty("Location");
            var locationValue = locationProperty.GetValue(createdResult.Value);

            Assert.Equal($"/products/{product.Id}", locationValue);
        }


        [Fact]
        public async Task ProductPost_ReturnError_PriceZero()
        {
            // Arrange

            // Crio um Dummie para categoria
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Category Dumie"
            };

            // Crio um Dummie de input dos dados
            var productRequestDTO = new ProductRequestDTO
            {
                Name = "Product Dumie",
                Description = "Description for product Dumie",
                Price = 0,
                Active = true,
                CategoryId = category.Id,
            };

            // Crio um Dummie para a saida Esperada de Product
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Category = category,
                Name = productRequestDTO.Name,
                Description = productRequestDTO.Description,
                Price = productRequestDTO.Price,
                CreatedBy = "doe joe",
                CreatedOn = DateTime.Now
            };

            // Crio comportamentos virtuais para as dependencias
            // utilizadas no EndPoint ProductPost
            // Eles vão enganar o endpoint no teste integrado

            //UnitOfWork ------------------------------------------------------------------
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            unitOfWorkMock.Setup(x => x.Categories.Get(It.IsAny<Guid>()))
                                                  .ReturnsAsync(category);

            unitOfWorkMock.Setup(x => x.Products.Add(It.IsAny<Product>()))
                                                .Callback<Product>(p => p.Id = product.Id);

            //Mapper ------------------------------------------------------------------
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Product>(It.IsAny<ProductRequestDTO>()))
                                    .Returns(product);

            mapperMock.Setup(x => x.Map<ProductResponseDTO>(It.IsAny<Product>()))
                                   .Returns(new ProductResponseDTO { Id = product.Id });

            //Logger ------------------------------------------------------------------
            var loggerMock = new Mock<ILogger<ProductController>>();

            //Controller------------------------------------------------------------------
            var productController = new ProductController(loggerMock.Object, mapperMock.Object, unitOfWorkMock.Object);

            // Act
            var result = await productController.ProductPost(productRequestDTO);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status400BadRequest, createdResult.StatusCode);
        }
    }
}