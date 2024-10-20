using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GerenciaPedidos.Infra.Data;

public class OrderRepository : IOrderRepository
{
    private readonly GerenciaPedidosContext _context;

    public OrderRepository(GerenciaPedidosContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetOrdersByDescriptionAsync(string description)
    {
        return await _context.Orders
            .Where(x => x.Description.Contains(description))
            .Include(x => x.Products)
            .ThenInclude(xx => xx.Product)
            .ToListAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(x => x.Products)
            .ThenInclude(xx => xx.Product)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int take, int skip)
    {
        return await _context.Orders
            .Include(x => x.Products)
            .ThenInclude(xx => xx.Product)
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(bool? open, int skip, int take)
    {
        var query = _context.Orders
            .Include(x => x.Products)
            .ThenInclude(xx => xx.Product)
            .Skip(skip)
            .Take(take);

        if (open.HasValue)
        {
            query = query.Where(x => x.Open == open.Value);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task CloseOrderAsync(Order order)
    {
        order.CloseOrder();
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task OpenOrderAsync(Order order)
    {
        order.OpenOrder();
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
}
