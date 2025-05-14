using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : Controller
{   
    private readonly AdminDbRepos _adminDbRepos;
    readonly ILogger<AdminController> _logger;

    public AdminController(ILogger<AdminController> logger, AdminDbRepos adminDbRepos)
    {
        _logger = logger;
        _adminDbRepos = adminDbRepos;
    }

    [HttpPost("createtestdata")]
    public async Task<IActionResult> CreateTestData()
    {
        try
        {
            await _adminDbRepos.CreateTestDataAsync();
            return Ok("Test data created successfully.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


}   
