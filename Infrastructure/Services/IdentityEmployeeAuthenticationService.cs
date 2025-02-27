using Application.Abstractions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

internal class IdentityEmployeeAuthenticationService : IEmployeeAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;

    public IdentityEmployeeAuthenticationService(
        UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> PasswordSignInAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new NotFoundException($"User with username '{username}' not found.");

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new PasswordIncorrectException();

        return user.Id;
    }

    public async Task<string[]> GetEmployeeRoles(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new NotFoundException($"User with username '{username}' not found.");

        var roles = await _userManager.GetRolesAsync(user);

        return [.. roles];
    }
}
