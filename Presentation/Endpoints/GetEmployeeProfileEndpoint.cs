
using Application.Abstractions;
using Application.DataTransferObjects.Employees;
using FastEndpoints;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Endpoints;

public class GetEmployeeProfileEndpoint : EndpointWithoutRequest<EmployeeProfileResponse>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeProfileEndpoint(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override void Configure()
    {
        Get("/api/user");
        Roles("Employee", "Administrator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? throw new UnauthorizedAccessException("User is not authenticated.");
        var employeeId = Guid.Parse(userIdClaim.Value);

        var profile = await _employeeService.GetEmployeeProfileAsync(employeeId);

        await SendAsync(profile);
    }
}
