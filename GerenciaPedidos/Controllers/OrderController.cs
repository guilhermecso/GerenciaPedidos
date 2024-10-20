using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Application.Services;
using GerenciaPedidos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GerenciaPedidos.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{

    private readonly ILogger<OrderController> _logger;
    private readonly OrderService _orderService;

    public OrderController(OrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Order))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
    {
        Order order = await _orderService.CreateOrderAsync(dto);
        return Ok(order);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    public async Task<IEnumerable<Order>> GetOrders([FromQuery] bool? open, [FromQuery] int take = 25, [FromQuery] int skip = 0)
    {
        IEnumerable<Order> orders = await _orderService.GetOrdersByStatusAsync(open, take, skip);
        return orders;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    public async Task<IActionResult> GetOrderById([FromRoute] int id)
    {
        Order order = await _orderService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return Ok(order);
    }

    [HttpPut]
    [Route("{id}/close-order")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    public async Task<IActionResult> CloseOrder([FromRoute] int id)
    {
        Order order = await _orderService.CloseOrderAsync(id);
        return Ok(order);
    }
    
    [HttpPut]
    [Route("{id}/open-order")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    public async Task<IActionResult> OpenOrder([FromRoute] int id)
    {
        Order order = await _orderService.OpenOrderAsync(id);
        return Ok(order);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Order))]
    public async Task<IActionResult> UpdateOrder([FromBody] CreateOrderDTO dto, [FromRoute] int id)
    {
        Order order = await _orderService.UpdateAsync(dto, id);
        return Ok(order);
    }
}
