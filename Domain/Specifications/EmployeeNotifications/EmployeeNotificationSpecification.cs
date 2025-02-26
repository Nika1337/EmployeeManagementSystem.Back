

using Ardalis.Specification;
using Domain.Models;

namespace Domain.Specifications.EmployeeNotifications;

public class EmployeeNotificationSpecification : SingleResultSpecification<EmployeeNotification>
{
    public EmployeeNotificationSpecification(Guid employeeId, Guid notificationId)
    {
        Query.Where(en => en.Employee.Id == employeeId && en.Notification.Id == notificationId);

        Query.AsTracking();
    }
}
