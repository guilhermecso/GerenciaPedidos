using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Domain.Entities;

public class Order
{
    public int Id { get; private set; }
    public string Description { get; set; }
    public decimal TotalValue { get; private set; }
    public DateTime OrderDate { get; private set; }
    public bool Open { get; set; } = true;

    private readonly List<OrderProduct> _products = new List<OrderProduct>();
    public ICollection<OrderProduct> Products => _products;

    public Order(string description)
    {
        OrderDate = DateTime.UtcNow;
        Description = description;
        TotalValue = 0;
    }

    public void AddProduct(Product product, int quantity)
    {
        var orderProduct = new OrderProduct(this, product, quantity);
        _products.Add(orderProduct);
        TotalValue += orderProduct.SubTotal;
    }

    public void CloseOrder()
    {
        Open = false;
    }

    public void OpenOrder()
    {
        Open = true;
    }
}
