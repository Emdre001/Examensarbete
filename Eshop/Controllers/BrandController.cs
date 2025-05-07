using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class BrandController : Controller
{
    readonly ILogger<BrandController> _logger;

    public BrandController(ILogger<BrandController> logger)
    {

        _logger = logger;
    }
}  