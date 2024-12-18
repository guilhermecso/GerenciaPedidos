using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Domain.Entities;
public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public int Stock { get; private set; }
    public DateTime CreationDate { get; private set; }

    public Product(string name, decimal value, int stock)
    {
        Name = name;
        Value = value;
        Stock = stock;
        CreationDate = DateTime.UtcNow;
    }

    public void DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ValidationException("Quantity must be greater than zero.");
        }

        if (quantity > Stock)
        {
            throw new ValidationException("Insufficient stock.");
        }

        Stock -= quantity;
    }

    public void IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ValidationException("Quantity must be greater than zero.");
        }

        Stock += quantity;
    }
}
