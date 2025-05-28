using System.Security.Claims;
using System.Text.Encodings.Web;
using DbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// https://www.youtube.com/watch?v=tW0YR-qogs8&ab_channel=ScriptBytes

namespace Eshop.Services;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly MainDbContext _context;

    public BasicAuthenticationHandler(
        MainDbContext context,
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _context = context;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        string authHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var token = authHeader["Basic ".Length..];
        var credentials = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(token)).Split(':');
        if (credentials.Length != 2)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }

        var username = credentials[0];
        var password = credentials[1];

        // Here you validate the username and password against the database.
        var user = await _context.Users
            .FirstAsync(u => u.UserName == username && u.Password == password);

        if (user == null)
        {
            return AuthenticateResult.Fail("Authentication failed");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserName),
            new(ClaimTypes.Role, user.Role)
        };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        return AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name));
    }
}