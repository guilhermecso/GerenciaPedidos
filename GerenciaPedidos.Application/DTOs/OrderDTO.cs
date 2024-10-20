using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Application.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public List<ProductDTO> Products { get; set; }
    public DateTime CreationDate { get; set; }
    public decimal TotalValue { get; set; }
    public bool Open { get; set; } = true;

    public OrderDTO()
    {
        Products = new List<ProductDTO>();
    }
}
