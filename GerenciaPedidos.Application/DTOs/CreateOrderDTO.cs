using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Application.DTOs;

public class CreateOrderDTO
{
    public string Description { get; set; }
    public List<int> ProductsIds { get; set; }
    public List<int> Quantities { get; set; }

    public CreateOrderDTO()
    {
        ProductsIds = new List<int>();
        Quantities = new List<int>();
    }
}
