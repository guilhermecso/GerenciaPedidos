using GerenciaPedidos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByDescriptionAsync(string description);
    Task<IEnumerable<Order>> GetOrdersByStatusAsync(bool? open, int skip, int take);
}
