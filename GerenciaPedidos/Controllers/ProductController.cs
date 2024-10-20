using GerenciaPedidos.Application.DTOs;
using GerenciaPedidos.Application.Services;
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
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO dto)
    {
        await _productService.CreateProductAsync(dto);
        return Ok();
    }

}
