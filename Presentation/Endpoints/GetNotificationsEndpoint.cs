
using FastEndpoints;
using Application.Abstractions;
using System.IdentityModel.Tokens.Jwt;


namespace Presentation.Endpoints;

public class NotificationResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required bool IsRead { get; init; }
}
public class GetNotificationsEndpoint : EndpointWithoutRequest<IEnumerable<NotificationResponse>>
{
    private readonly INotificationService _notificationService;

    public GetNotificationsEndpoint(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public override void Configure()
    {
        Get("/api/messages");
        Roles("Employee", "Administrator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirst(JwtRegisteredClaimNames.Sub) ?? throw new UnauthorizedAccessException("User is not authenticated.");
        var employeeId = Guid.Parse(userIdClaim.Value);

        var notifications = await _notificationService.GetNotificationsOfEmployeeAsync(employeeId);

        var response = notifications.Select(notification => new NotificationResponse
        {
            Id = notification.Id,
            Title = notification.Title,
            Text = notification.Text,
            CreatedAt = notification.CreatedAt,
            IsRead = notification.IsRead
        });

        await SendAsync(response);
    }
}
