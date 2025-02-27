
using FastEndpoints;
using Application.Abstractions;
using Domain.Exceptions;


namespace Presentation.Endpoints;

public class LoginRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}

public class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IEmployeeAuthenticationService _authService;

    public LoginEndpoint(IEmployeeAuthenticationService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        string id;
        try
        {
            id = await _authService.PasswordSignInAsync(req.Username, req.Password);
        }
        catch (PasswordIncorrectException)
        {
            await SendUnauthorizedAsync();
            return;
        }
        catch (NotFoundException)
        {
            await SendUnauthorizedAsync();
            return;
        }

        string[] roles = await _authService.GetEmployeeRoles(req.Username);

        string token = AuthorizationExtensions.GenerateToken(id, req.Username, roles);

        await SendAsync(new { Token = token });
    }
}
