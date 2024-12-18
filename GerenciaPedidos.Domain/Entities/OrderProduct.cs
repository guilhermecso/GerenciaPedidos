using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Domain.Entities;

public class OrderProduct
{
    public int OrderId { get; private set; }
    public Order Order { get; private set; }

    public int ProductId { get; private set; }
    public Product Product { get; private set; }

    public int Quantity { get; private set; }
    public decimal SubTotal { get; private set; }

    public OrderProduct(Order order, Product product, int quantity)
    {
        Order = order;
        Product = product;
        Quantity = quantity;
        SubTotal = product.Value * Quantity;
    }
}
