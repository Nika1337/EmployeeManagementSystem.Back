

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

internal class EmployeeNotificationConfiguration : IEntityTypeConfiguration<EmployeeNotification>
{
    public void Configure(EntityTypeBuilder<EmployeeNotification> builder)
    {
        builder.HasKey("EmployeeId", "NotificationId");

        builder.HasOne(en => en.Employee)
            .WithMany(e => e.Notifications)
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(en => en.Notification)
            .WithMany(n => n.Recipients)
            .HasForeignKey("NotificationId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
