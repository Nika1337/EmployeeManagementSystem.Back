
namespace Domain.Models;

public class Notification
{
    public Guid Id { get; private init; }
    public string Title { get; private init; }
    public string Text { get; private init; }
    public DateTime CreatedAtUtc { get; private init; }
    public bool IsRead { get; private set; }

    #pragma warning disable CS8618 // needed for ef core
    private Notification() { }
    #pragma warning restore CS8618

    public Notification(string title, string text)
    {
        Id = Guid.NewGuid();
        Title = title;
        Text = text;
        CreatedAtUtc = DateTime.UtcNow;
        IsRead = false;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }
}
