
namespace Application.DataTransferObjects.Notifications;

public record NotificationResponse
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required bool IsRead { get; init; }
}
