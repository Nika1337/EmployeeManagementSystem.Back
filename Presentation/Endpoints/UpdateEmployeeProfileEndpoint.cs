
using FastEndpoints;
using Application.Abstractions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Endpoints;

public class EmployeeProfileUpdateRequest {
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTime BirthDateUtc { get; init; }
}
public class UpdateEmployeeProfileEndpoint : Endpoint<EmployeeProfileUpdateRequest>
{
    private readonly IEmployeeService _employeeService;

    public UpdateEmployeeProfileEndpoint(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override void Configure()
    {
        Put("/api/user");
        Roles("Employee", "Administrator");
    }

    public override async Task HandleAsync(EmployeeProfileUpdateRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? throw new UnauthorizedAccessException("User is not authenticated.");
        var employeeId = Guid.Parse(userIdClaim.Value);

        var serviceRequest = new Application.DataTransferObjects.Employees.EmployeeProfileUpdateRequest
        {
            EmployeeId = employeeId,
            FirstName = req.FirstName,
            LastName = req.LastName,
            BirthDateUtc = req.BirthDateUtc,
        };

        await _employeeService.UpdateEmployeeProfileAsync(serviceRequest);
        await SendNoContentAsync();
    }
}
