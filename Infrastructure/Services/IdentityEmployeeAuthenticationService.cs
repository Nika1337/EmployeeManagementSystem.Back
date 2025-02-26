using Application.Abstractions;
using Domain.Exceptions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

internal class IdentityEmployeeAuthenticationService : IEmployeeAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public IdentityEmployeeAuthenticationService(
        UserManager<IdentityUser> userManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
    }
    public async Task<string> PasswordSignInAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new NotFoundException($"User with username '{username}' not found.");

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new PasswordIncorrectException();

        return await GenerateJwtTokenAsync(user);
    }

    private async Task<string> GenerateJwtTokenAsync(IdentityUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.UserName ?? throw new ApplicationException($"Employee with id '{user.Id}' doesn't have username.")),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            signingCredentials: creds);

        return _tokenHandler.WriteToken(token);
    }
}
