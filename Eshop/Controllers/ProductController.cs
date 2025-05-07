using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : Controller
{
    readonly IProductService _service = null;
    readonly ILogger<ProductController> _logger = null;

    public ProductController(IProductService service, ILogger<ProductController> logger)
    {
        _service = service;
        _logger = logger;
    }
}   
