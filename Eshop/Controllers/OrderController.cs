using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;


namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class OrderController : Controller
{
    readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
    {

        _logger = logger;
    }



    
}   
