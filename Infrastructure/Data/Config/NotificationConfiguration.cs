

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(n => n.Text)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(n => n.CreatedAtUtc)
            .IsRequired();

        builder.HasMany(n => n.Recipients)
            .WithOne(en => en.Notification)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
