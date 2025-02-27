
using FastEndpoints;
using Application.Abstractions;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Presentation.Endpoints;


public class MarkNotificationAsReadRequest
{
    public Guid NotificationId { get; set; }
}

public class MarkNotificationAsReadEndpoint : Endpoint<MarkNotificationAsReadRequest>
{
    private readonly INotificationService _notificationService;

    public MarkNotificationAsReadEndpoint(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override void Configure()
    {
        Put("/api/messages/{NotificationId}");
        Description(x => x.Accepts<MarkNotificationAsReadRequest>());
        Roles("Employee", "Administrator");
    }

    public override async Task HandleAsync(MarkNotificationAsReadRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? throw new UnauthorizedAccessException("User is not authenticated.");
        var employeeId = Guid.Parse(userIdClaim.Value);

        await _notificationService.MarkAsReadAsync(employeeId, req.NotificationId);

        await SendNoContentAsync();
    }
}
