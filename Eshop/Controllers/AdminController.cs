using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

using DbRepos;


namespace Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
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
    
    [HttpDelete("deleteall")]
    public async Task<IActionResult> DeleteAllData()
    {
        try
        {
            await _adminDbRepos.DeleteAllDataAsync();
            return Ok("All data deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to delete all data");
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


}   
