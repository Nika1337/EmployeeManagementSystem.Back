

namespace Domain.Models;

public class EmployeeNotification
{
    public Employee Employee { get; private init; }
    public Notification Notification { get; private init; }

    public bool IsRead { get; private set; }

    #pragma warning disable CS8618 // needed for ef core
    private EmployeeNotification() { }
    #pragma warning restore CS8618

    public EmployeeNotification(Employee employee, Notification notification)
    {
        Employee = employee;
        Notification = notification;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        if (!IsRead)
        {
            IsRead = true;
        }
    }
}
