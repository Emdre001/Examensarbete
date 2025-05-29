using System.Security.Claims;
using DbContext;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

// https://www.youtube.com/watch?v=XKhRfZkZiow&t=90s&ab_channel=ScriptBytes

namespace Eshop.Services;

public class ClaimsTransformationService : IClaimsTransformation
{
    public readonly MainDbContext _context;

    public ClaimsTransformationService(MainDbContext context)
    {
        _context = context;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.Identity.IsAuthenticated != true)
        {
            return principal;
        }

        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
        var userRole = await _context.Users
            .Where(u => u.UserName == userId)
            .Select(u => u.Role)
        .SingleOrDefaultAsync();

        if (string.IsNullOrEmpty(userRole))
        {
            return principal;
        }

        if (!principal.HasClaim(ClaimTypes.Role, userRole))
        {
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim(ClaimTypes.Role, userRole));
        }

        return principal;
    }
}