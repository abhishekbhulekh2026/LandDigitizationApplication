using Microsoft.AspNetCore.Http;
using System.Security.Claims;

public class JwtUserHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JwtUserHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long GetLoggedInUserId()
    {
        var userId = _httpContextAccessor.HttpContext?
    .User?
    .FindFirst("UserID")?.Value;

        return string.IsNullOrEmpty(userId) ? 0 : Convert.ToInt64(userId);
    }

    public string GetLoggedInRole()
    {
        return _httpContextAccessor.HttpContext?
            .User?
            .FindFirst(ClaimTypes.Role)?.Value ?? "";
    }
}