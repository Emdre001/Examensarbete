using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Text;
using DbContext;
using Models.DTO;
using Microsoft.EntityFrameworkCore;

public class JwtService
{
    private readonly IConfiguration _config;
    private readonly MainDbContext  _context;

    public JwtService(IConfiguration config, MainDbContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task<LoginResponseModel?> Authenticate(LoginRequestModel loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return null;
        }

        var userAccount = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginRequest.Username);

        if (userAccount is null || userAccount.Password != loginRequest.Password)
        {
            return null; // Invalid credentials
        }

        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = _config["Jwt:Key"];
        var TokenValidityMins = _config.GetValue<int>("Jwt:TokenValidityMins");
        var tokenExpiration = DateTime.UtcNow.AddMinutes(TokenValidityMins);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Role, userAccount.Role)
            ]),
            Expires = tokenExpiration,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(securityToken);

        return new LoginResponseModel
        {
            UserName = loginRequest.Username,
            AccessToken = accessToken,
            ExpiresIn = (int)tokenExpiration.Subtract(DateTime.UtcNow).TotalSeconds
        };
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Role, user.Role)
        };

        var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}