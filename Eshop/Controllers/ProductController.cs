using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : Controller
{
    readonly ILogger<ProductController> _logger = null;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }



    
}   
