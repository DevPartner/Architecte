using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CartService.Infrastructure.Services;
public class UserService : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        var context = _httpContextAccessor.HttpContext;
        if (context != null && context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            var accessToken = token.ToString().Substring("Bearer ".Length).Trim();
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(accessToken);

            if (jwtToken != null)
            {
                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                var userRole = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
                Id = userId;
                Role = userRole;
            }
        }
    }

    public string? Id { get; init; }
    public string? Role { get; init; }
}
