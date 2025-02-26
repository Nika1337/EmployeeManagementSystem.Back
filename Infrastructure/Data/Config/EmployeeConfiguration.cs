

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config;

internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Position)
            .HasMaxLength(100);

        builder.Property(e => e.Department)
            .HasMaxLength(100);

        builder.HasMany(e => e.Notifications)
            .WithOne(en => en.Employee)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
