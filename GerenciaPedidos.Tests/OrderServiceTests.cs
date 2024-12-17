using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Application.Services;
using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;
using Moq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace GerenciaPedidos.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _orderService = new OrderService(_orderRepositoryMock.Object, _productRepositoryMock.Object);
        }

        [Fact]
        public async Task Test_CreateOrderAsync_ShouldCreateOrder()
        {
            // Arrange
            var createOrderDTO = new CreateOrderDTO
            {
                Description = "Test Order",
                ProductsIds = new List<int> { 1 },
                Quantities = new List<int> { 2 }
            };

            var product = new Product("Test Product", 10, 5);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _orderService.CreateOrderAsync(createOrderDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Order", result.Description);
            Assert.Single(result.Products);
            Assert.Equal(20, result.TotalValue);
        }

        [Fact]
        public async Task Test_GetAllAsync_ShouldReturnAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order("Order 1"),
                new Order("Order 2")
            };

            _orderRepositoryMock.Setup(repo => repo.GetAllAsync(2, 0)).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllAsync(2, 0);

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Test_GetOrdersByStatusAsync_ShouldReturnOrdersByStatus()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order("Order 1") { Open = true },
                new Order("Order 2") { Open = false }
            };

            _orderRepositoryMock.Setup(repo => repo.GetOrdersByStatusAsync(true, 0, 2)).ReturnsAsync(orders.Where(o => o.Open));

            // Act
            var result = await _orderService.GetOrdersByStatusAsync(true, 2, 0);

            // Assert
            Assert.Single(result);
            Assert.True(result.First().Open);
        }

        [Fact]
        public async Task Test_UpdateAsync_ShouldUpdateOrder()
        {
            // Arrange
            var createOrderDTO = new CreateOrderDTO
            {
                Description = "Updated Order",
                ProductsIds = new List<int> { 1 },
                Quantities = new List<int> { 2 }
            };

            var order = new Order("Test Order");
            var product = new Product("Test Product", 10, 5);

            _orderRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(order);
            _productRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _orderService.UpdateAsync(createOrderDTO, 1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Order", result.Description);
            Assert.Single(result.Products);
            Assert.Equal(20, result.TotalValue);
        }
    }
}
