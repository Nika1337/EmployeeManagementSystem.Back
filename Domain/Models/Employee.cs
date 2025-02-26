

namespace Domain.Models;

public class Employee
{
    public Guid Id { get; private init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Position { get; private set; }
    public string Department { get; private set; }

    private readonly List<EmployeeNotification> _notifications = [];

    public IReadOnlyCollection<EmployeeNotification> Notifications => _notifications.AsReadOnly();

    #pragma warning disable CS8618 // Required for ef core
    private Employee() { }
    #pragma warning restore CS8618 

    public Employee(string firstName, string lastName, string position, string department)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Department = department;
    }

    public void Update(string firstName, string lastName, string position, string department)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Department = department;
    }

    public void AssignNotification(Notification notification)
    {
        var employeeNotification = new EmployeeNotification(this, notification);

        _notifications.Add(employeeNotification);
    }
}
