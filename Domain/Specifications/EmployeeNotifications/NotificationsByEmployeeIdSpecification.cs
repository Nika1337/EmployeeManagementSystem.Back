using Ardalis.Specification;
using Domain.Models;
using Domain.Specifications.EmployeeNotifications.Results;


namespace Domain.Specifications.EmployeeNotifications;

public class NotificationsByEmployeeIdSpecification : Specification<EmployeeNotification, NotificationResult>
{
    public NotificationsByEmployeeIdSpecification(Guid employeeId)
    {
        Query.Where(en => en.Employee.Id == employeeId);

        Query.Select(en => new NotificationResult
        {
            Id = en.Notification.Id,
            Title = en.Notification.Title,
            Text = en.Notification.Text,
            CreatedAt = en.Notification.CreatedAtUtc,
            IsRead = en.IsRead,
        });

        Query.AsNoTracking();
    }
}
