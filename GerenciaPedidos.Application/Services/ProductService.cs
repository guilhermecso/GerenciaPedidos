using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;

namespace GerenciaPedidos.Application.Services;

public class ProductService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public ProductService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task CreateProductAsync(CreateProductDTO dto)
    {
        var product = new Product(dto.Name, dto.Value, dto.Stock);
        await _productRepository.AddAsync(product);
    }
}