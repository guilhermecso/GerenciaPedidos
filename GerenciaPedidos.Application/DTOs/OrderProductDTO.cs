using GerenciaPedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Application.DTOs;

public class OrderProductDTO
{
    public string Name { get; set; }
    public decimal Value { get; set; }
    public int SubTotal { get; set; }

}
