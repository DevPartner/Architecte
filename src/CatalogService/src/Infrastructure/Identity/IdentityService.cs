using CatalogService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CatalogService.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    //private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAuthorizationService _authorizationService;

    public IdentityService(
        //UserManager<ApplicationUser> userManager,
        IAuthorizationService authorizationService)
    {
        //_userManager = userManager;
        _authorizationService = authorizationService;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        /*var user await _userManager.FindByIdAsync(userId);

        return user?.UserName;*/
        string? result = null;
        return await Task.FromResult(result);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        /*var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);*/
        bool result = false;
        return await Task.FromResult(result);

    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        bool result = false;
        return await Task.FromResult(result);
        /*var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;*/
    }
}
