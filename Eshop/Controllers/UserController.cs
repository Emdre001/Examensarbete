using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using DbRepos;
using DbContext;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Models;

namespace Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    private readonly MainDbContext _context;
    private readonly IConfiguration _configuration;
    public UserController(MainDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserEmail == loginDto.Email && u.UserPassword == loginDto.Password);
        if (user == null)
        {
            return Unauthorized("Invalid email or password.");
        }

        var jwtSettings = _configuration.GetSection("Jwt");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.UserEmail),
            new Claim(ClaimTypes.Role, user.UserRole)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    private readonly ILogger<UserController> _logger;
    private readonly UserDbRepos _userRepo;

    public UserController(ILogger<UserController> logger, UserDbRepos userRepo)
    {
        _logger = logger;
        _userRepo = userRepo;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (_context.Users.Any(u => u.UserEmail == dto.Email))
            return BadRequest("Email already registered.");

        byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: dto.Password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));

        var user = new UserDTO
        {
            UserId = Guid.NewGuid(),
            UserEmail = dto.Email,
            UserPassword = hashed,
            UserRole = dto.Role ?? "User", // Default to "User"
            UserSalt = Convert.ToBase64String(salt)
        };

        var userEntity = new User
        {
            UserId = user.UserId,
            UserName = user.UserName,
            UserEmail = user.UserEmail,
            UserPassword = user.UserPassword,
            UserAddress = user.UserAddress,
            UserPhoneNr = user.UserPhoneNr,
            UserRole = user.UserRole,
            Orders = []
        };

        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
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