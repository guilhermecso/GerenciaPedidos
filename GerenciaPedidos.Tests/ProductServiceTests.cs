using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Application.Services;
using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;
using Moq;
using Xunit;

namespace GerenciaPedidos.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _productService = new ProductService(_mockOrderRepository.Object, _mockProductRepository.Object);
        }

        [Fact]
        public async Task Test_CreateProductAsync_ShouldCreateProduct()
        {
            // Arrange
            var createProductDTO = new CreateProductDTO
            {
                Name = "Test Product",
                Value = 10.0m,
                Stock = 5
            };

            var product = new Product(createProductDTO.Name, createProductDTO.Value, createProductDTO.Stock);

            _mockProductRepository.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(Task.CompletedTask);

            // Act
            var result = await _productService.CreateProductAsync(createProductDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createProductDTO.Name, result.Name);
            Assert.Equal(createProductDTO.Value, result.Value);
            Assert.Equal(createProductDTO.Stock, result.Stock);
        }

        [Fact]
        public async Task Test_GetAllAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product("Product 1", 10.0m, 5),
                new Product("Product 2", 20.0m, 10)
            };

            _mockProductRepository.Setup(repo => repo.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllAsync(10, 0);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
