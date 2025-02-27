

namespace Domain.Models;

public class Employee
{
    public Guid Id { get; private init; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Position { get; private set; }
    public string Department { get; private set; }
    public DateTime BirthDateUtc { get; private set; }
    public DateTime StartDateUtc { get; private init; }


    private readonly List<EmployeeNotification> _notifications = [];

    public IReadOnlyCollection<EmployeeNotification> Notifications => _notifications.AsReadOnly();

    #pragma warning disable CS8618 // Required for ef core
    private Employee() { }
    #pragma warning restore CS8618 

    public Employee(string firstName, string lastName, string position, string department, DateTime birthDate)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Department = department;
        BirthDateUtc = birthDate;
        StartDateUtc = DateTime.UtcNow;
    }

    public void UpdateFirstName(string firstName)
    {
        FirstName = firstName;
    }

    public void UpdateLastName(string lastName)
    {
        LastName = lastName;
    }
    public void UpdateBirthDate(DateTime birthDate)
    {
        BirthDateUtc = birthDate;
    }

    public void TransferToDepartment(string department)
    {
        Department = department;
    }

    public void ChangePosition(string position)
    {
        Position = position;
    }

    public void AssignNotification(Notification notification)
    {
        var employeeNotification = new EmployeeNotification(this, notification);

        _notifications.Add(employeeNotification);
    }
}
