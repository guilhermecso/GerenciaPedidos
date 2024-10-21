using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Application.Services;
using GerenciaPedidos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GerenciaPedidos.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly ProductService _productService;

    public ProductController(ProductService productService, ILogger<OrderController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
    {
        Product product = await _productService.CreateProductAsync(dto);
        return Ok(product);
    }

    [HttpGet]
    public async Task<IEnumerable<Product>> GetProductsAsync([FromQuery] int take = 25, [FromQuery] int skip = 0)
    {
        IEnumerable<Product> products = await _productService.GetAllAsync(take, skip);
        return products;
    }

}
