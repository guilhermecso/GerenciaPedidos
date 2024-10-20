using GerenciaPedidos.Domain.Entities;

namespace GerenciaPedidos.Tests
{
    public class OrderTest
    {
        [Fact]
        public void Test_AddProduct_ShouldIncreaseTotalValue()
        {
            var order = new Order("Test Order");
            var product = new Product("Test Product", 10, 5);
            var quantity = 2;

            order.AddProduct(product, quantity);

            Assert.Equal(20, order.TotalValue);
        }

        [Fact]
        public void Test_CloseOrder_ShouldSetOpenToFalse()
        {
            var order = new Order("Test Order");

            order.CloseOrder();

            Assert.False(order.Open);
        }

        [Fact]
        public void Test_OpenOrder_ShouldSetOpenToTrue()
        {
            var order = new Order("Test Order");

            order.OpenOrder();

            Assert.True(order.Open);
        }
    }
}