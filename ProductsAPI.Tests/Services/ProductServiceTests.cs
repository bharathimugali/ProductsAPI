using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ProductsAPI.Data.Entities;
using ProductsAPI.Repositories;
using ProductsAPI.Services;

namespace ProductsAPI.Tests.Services
{
    public class ProductServiceTests
    {
        private Mock<IRepository<Product>> _mockRepository;
        private Mock<ILogger<ProductService>> _mockLogger;
        private Mock<IMapper> _mockMapper;
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new Mock<IRepository<Product>>();
            _mockLogger = new Mock<ILogger<ProductService>>();
            _mockMapper = new Mock<IMapper>();

            _productService = new ProductService(_mockRepository.Object, _mockMapper.Object, _mockLogger.Object);
        }

        [Test]
        public async Task DecrementStockAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            int productId = 1;
            int quantity = 5;
            var product = new Product { Id = productId, Name = "Test Product", Stock = 12 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
                           .ReturnsAsync(product);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _productService.DecrementStockAsync(productId, quantity);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(7, product.Stock);
            _mockRepository.Verify(repo => repo.UpdateAsync(product), Times.Once);
        }

        [Test]
        public async Task AddToStockAsync_ValidRequest_ReturnsTrue()
        {
            // Arrange
            int productId = 1;
            int quantity = 5;
            var product = new Product { Id = productId, Name = "Test Product", Stock = 12 };

            _mockRepository.Setup(repo => repo.GetByIdAsync(productId))
                           .ReturnsAsync(product);
            _mockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _productService.AddToStockAsync(productId, quantity);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(17, product.Stock);
            _mockRepository.Verify(repo => repo.UpdateAsync(product), Times.Once);
        }
    }
}
