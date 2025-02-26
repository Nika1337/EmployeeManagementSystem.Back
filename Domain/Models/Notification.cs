
namespace Domain.Models;

public class Notification
{
    public Guid Id { get; private init; }
    public string Title { get; private init; }
    public string Text { get; private init; }
    public DateTime CreatedAtUtc { get; private init; }

    private readonly List<EmployeeNotification> _recipients = [];

    public IReadOnlyCollection<EmployeeNotification> Recipients => _recipients.AsReadOnly();

    #pragma warning disable CS8618 // needed for ef core
    private Notification() { }
    #pragma warning restore CS8618

    public Notification(string title, string text)
    {
        Id = Guid.NewGuid();
        Title = title;
        Text = text;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void AssignRecipient(Employee employee)
    {
        var employeeNotification = new EmployeeNotification(employee, this);

        _recipients.Add(employeeNotification);
    }
}
