

namespace Application.Abstractions;

public interface IEmployeeAuthenticationService
{
    Task<string> PasswordSignInAsync(string username, string password);
    Task<string[]> GetEmployeeRoles(string username);
}
