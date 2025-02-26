using Domain.Models;
using Infrastructure.Data.Config;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;

internal class DomainDbContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<EmployeeNotification> EmployeeNotifications { get; set; }

    public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new EmployeeConfiguration());
        builder.ApplyConfiguration(new NotificationConfiguration());
        builder.ApplyConfiguration(new EmployeeNotificationConfiguration());
    }
}
