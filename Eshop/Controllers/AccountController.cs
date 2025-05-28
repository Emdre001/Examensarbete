using Eshop.DbRepos;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly AccountRepos _registrationRepos;

    public AccountController(JwtService jwtService, AccountRepos registrationRepos)
    {
        _jwtService = jwtService;
        _registrationRepos = registrationRepos;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponseModel>> Login([FromBody] LoginRequestModel request)
    {
        var result = await _jwtService.Authenticate(request);
        if (result == null)
            return Unauthorized();

        return result;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var user = new User
        {
            UserName = dto.UserName,
            Password = dto.Password,
            Email = dto.Email,
            Role = "User"
        };

        var result = await _registrationRepos.RegisterAsync(user);
        if (!result)
            return BadRequest("Registration failed. Please try again.");

        return Ok("User registered successfully.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts()
    {
        var users = await _registrationRepos.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccountById(Guid id)
    {
        var user = await _registrationRepos.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountDto dto)
    {
        var updated = await _registrationRepos.UpdateUserAsync(id, dto);
        if (!updated)
            return NotFound();
        return Ok("User updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        var deleted = await _registrationRepos.DeleteUserAsync(id);
        if (!deleted)
            return NotFound();
        return Ok("User deleted successfully.");
    }
}