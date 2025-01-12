using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductsAPI.Controllers;
using ProductsAPI.Infrastructure.Exceptions;
using ProductsAPI.Models.ResponseDto;
using ProductsAPI.Services;

namespace ProductsAPI.Tests.Controllers
{
        [TestFixture]
        public class ProductsControllerTests
        {
            private Mock<IProductService> _mockProductService;
            private ProductsController _controller;

            [SetUp]
            public void Setup()
            {
                _mockProductService = new Mock<IProductService>();
                _controller = new ProductsController(_mockProductService.Object);
            }

            [Test]
            public async Task GetAll_ReturnsOkResult_WithListOfProducts()
            {
                // Arrange
                var mockProducts = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product1", Stock = 10 },
                new ProductDto { Id = 2, Name = "Product2", Stock = 5 }
            };

                _mockProductService
                    .Setup(service => service.GetAllProductsAsync())
                    .ReturnsAsync(mockProducts);

                // Act
                var result = await _controller.GetAll();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(result);
               // Assert.IsInstanceOf<List<ProductDto>>(result.Value);

                var products = okResult.Value as List<ProductDto>;
                Assert.AreEqual(2, products.Count);
            }

            [Test]
            public async Task GetAll_ReturnsOkResult_WithEmptyList_WhenNoProductsExist()
            {
                // Arrange
                _mockProductService
                    .Setup(service => service.GetAllProductsAsync())
                    .ReturnsAsync(new List<ProductDto>());

                // Act
                var result = await _controller.GetAll();

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);

                var okResult = result as OkObjectResult;
                Assert.IsNotNull(okResult);
                Assert.IsInstanceOf<List<ProductDto>>(okResult.Value);

                var products = okResult.Value as List<ProductDto>;
                Assert.AreEqual(0, products.Count);
            }

        [Test]
        public async Task GetProductById_ProductExists_ReturnsOkWithProduct()
        {
            // Arrange
            int productId = 1;
            var expectedProduct = new ProductDto { Id = productId, Name = "Sample Product", Stock = 10 };
            _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                               .ReturnsAsync(expectedProduct);

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);
            var productDto = okResult.Value as ProductDto;
            Assert.IsNotNull(productDto);
            Assert.AreEqual(expectedProduct.Id, productDto.Id);
            Assert.AreEqual(expectedProduct.Name, productDto.Name);
        }

        [Test]
        public async Task GetProductById_ProductDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            int productId = 1;
           _mockProductService.Setup(service => service.GetProductByIdAsync(productId))
                       .ThrowsAsync(new NotFoundException($"Product with ID {productId} not found."));

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundObjectResult);
           Assert.AreEqual(StatusCodes.Status404NotFound, notFoundObjectResult.StatusCode);
        }

    }
}
