

using Application.DataTransferObjects.Notifications;

namespace Application.Abstractions;

public interface INotificationService
{
    Task<IEnumerable<NotificationResponse>> GetNotificationsOfEmployeeAsync(Guid employeeId);
    Task MarkAsReadAsync(Guid employeeId, Guid notificationId);
}
