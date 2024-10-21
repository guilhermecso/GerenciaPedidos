using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciaPedidos.Infra.Data;

public class ProductRepository : IProductRepository
{
    private readonly GerenciaPedidosContext _context;

    public ProductRepository(GerenciaPedidosContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Product>> GetAllAsync(int take, int skip)
    {
        return await _context.Products
            .Skip(skip)
            .Take(take)
            .ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<IEnumerable<Product>> GetProductsByDescriptionAsync(string description)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
