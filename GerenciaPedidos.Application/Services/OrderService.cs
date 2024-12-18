using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Domain.Entities;
using GerenciaPedidos.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace GerenciaPedidos.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public async Task<Order> CreateOrderAsync(CreateOrderDTO dto)
    {
        var order = new Order(dto.Description);
        for (int i = 0; i < dto.ProductsIds.Count; i++)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductsIds[i]);
            order.AddProduct(product, dto.Quantities[i]);
        }

        await _orderRepository.AddAsync(order);

        return order;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(int take, int skip)
    {
        return await _orderRepository.GetAllAsync(take, skip);
    }

    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(bool? status, int take, int skip)
    {
        return await _orderRepository.GetOrdersByStatusAsync(status, skip, take);
    }

    public async Task<Order> UpdateAsync(CreateOrderDTO dto, int id)
    {
        var order = await _orderRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Order not found");

        if (order.Open == false)
        {
            throw new ValidationException("The order is already closed.");
        }

        order.Description = dto.Description;

        order.Products.Clear();

        for (int i = 0; i < dto.ProductsIds.Count; i++)
        {
            var product = await _productRepository.GetByIdAsync(dto.ProductsIds[i]);
            order.AddProduct(product, dto.Quantities[i]);
        }

        await _orderRepository.UpdateAsync(order);

        return order;
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ValidationException(message: "Id must be greater than 0");
        }

        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<Order> CloseOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Order not found");
        if (order.Products.Count <= 0)
        {
            throw new ValidationException("The order does not have any products.");
        }

        if (order.Open == false)
        {
            throw new ValidationException("The order is already closed.");
        }

        foreach (var orderProduct in order.Products)
        {
            orderProduct.Product.DecreaseStock(orderProduct.Quantity);
        }

        order.CloseOrder();
        await _orderRepository.UpdateAsync(order);

        return order;
    }
    public async Task<Order> OpenOrderAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Order not found");

        if (order.Open == true)
        {
            throw new ValidationException("The order is already open.");
        }

        foreach (var orderProduct in order.Products)
        {
            orderProduct.Product.IncreaseStock(orderProduct.Quantity);
        }

        order.OpenOrder();
        await _orderRepository.UpdateAsync(order);

        return order;
    }
}
