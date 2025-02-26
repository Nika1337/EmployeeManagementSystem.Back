
using Application.Abstractions;
using Application.DataTransferObjects.Notifications;
using Domain.Abstractions;
using Domain.Exceptions;
using Domain.Models;
using Domain.Specifications.EmployeeNotifications;

namespace Infrastructure.Services;

internal class NotificationService : INotificationService
{
    private readonly IRepository<EmployeeNotification> _repository;

    public NotificationService(IRepository<EmployeeNotification> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NotificationResponse>> GetNotificationsOfEmployeeAsync(Guid employeeId)
    {
        var specification = new NotificationsByEmployeeIdSpecification(employeeId);

        var notifications = await _repository.ListAsync(specification);

        var response = notifications.Select(notification => new NotificationResponse
        {
            Id = notification.Id,
            Title = notification.Title,
            Text = notification.Text,
            CreatedAt = notification.CreatedAt,
            IsRead = notification.IsRead,
        });

        return response;
    }

    public async Task MarkAsReadAsync(Guid employeeId, Guid notificationId)
    {
        var specification = new EmployeeNotificationSpecification(employeeId, notificationId);

        var employeeNotification = await _repository.SingleOrDefaultAsync(specification)
            ?? throw new NotFoundException($"Notification with id '{notificationId}' for employee with id '{employeeId}' not found.");

        employeeNotification.MarkAsRead();

        await _repository.UpdateAsync(employeeNotification);
    }
}
