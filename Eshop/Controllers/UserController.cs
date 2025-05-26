using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using DbRepos;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly UserDbRepos _userRepo;

    public UserController(ILogger<UserController> logger, UserDbRepos userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDTO dto)
    {
        var user = await _userRepo.CreateUserAsync(dto);
        return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userRepo.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userRepo.GetUserByIdAsync(id);
        if (user == null) return NotFound("User not found.");
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UserDTO dto)
    {
        var success = await _userRepo.UpdateUserAsync(id, dto);
        if (!success) return NotFound("User not found for update.");
        return Ok("User updated.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _userRepo.DeleteUserAsync(id);
        if (!success) return NotFound("User not found for deletion.");
        return Ok("User deleted.");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteAll()
    {
        await _userRepo.DeleteAllUsersAsync();
        return Ok("All users deleted.");
    }

}