using System.Security.Claims;
using Eshop.DbRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : ControllerBase
{
    private readonly AccountRepos _accountRepos;

    public AccountController(AccountRepos accountRepos) => _accountRepos = accountRepos;

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel dto)
    {
        var user = await _accountRepos.LoginAsync(dto.UsernameOrEmail, dto.Password);
        if (user == null)
            return Unauthorized("Invalid Username/Email or password.");

        return Ok(new { user.UserName, user.Role });
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

        var result = await _accountRepos.RegisterAsync(user);
        if (!result)
            return BadRequest("Registration failed. Please try again.");

        return Ok("User registered successfully.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AccountDto>>> GetAllAccounts()
    {
        var users = await _accountRepos.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccountById(Guid id)
    {
        var user = await _accountRepos.GetUserByIdAsync(id);
        if (user == null)
            return NotFound();
        return Ok(user);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetCurrentUser()
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr))
            return Unauthorized();

        var userEntity = await _accountRepos.GetUserByNameAsync(userIdStr);
        if (userEntity == null)
            return NotFound();

        var user = await _accountRepos.GetUserByIdAsync(userEntity.UserId);
        if (user == null)
            return NotFound();

        var userDto = new 
        {
            user.UserId,
            user.UserName,
            user.Password,
            user.Email,
            user.Address,
            user.PhoneNr,
            user.Role
        };

        return Ok(userDto);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] AccountDto dto)
    {
        var updated = await _accountRepos.UpdateUserAsync(id, dto);
        if (!updated)
            return NotFound();
        return Ok("User updated successfully.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAccount(Guid id)
    {
        var deleted = await _accountRepos.DeleteUserAsync(id);
        if (!deleted)
            return NotFound();
        return Ok("User deleted successfully.");
    }
}