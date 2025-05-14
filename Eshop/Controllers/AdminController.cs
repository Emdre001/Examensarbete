using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Services;
using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{   
    private readonly AdminDbRepos _adminDbRepos = null;
    readonly ILogger<AdminController> _logger = null;

    public AdminController(ILogger<AdminController> logger)
    {
        _logger = logger;
    }

    [HttpPost("createtestdata")]
        public IActionResult CreateTestData()
        {
            try
            {
                // Call the CreateTestData method from the AdminDbRepo
                _adminDbRepos.CreateTestData();

                // Return a success response
                return Ok("Test data created successfully.");
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


}   
