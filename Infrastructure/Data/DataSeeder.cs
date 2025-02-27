

using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<DomainDbContext>();

        await SeedRolesAsync(roleManager);
        await SeedEmployeesAndUsersAsync(userManager, dbContext);
        await SeedNotificationsAsync(dbContext);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["Employee", "Administrator"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    private static async Task SeedEmployeesAndUsersAsync(UserManager<IdentityUser> userManager, DomainDbContext dbContext)
    {
        if (await dbContext.Employees.AnyAsync()) return;

        var david = new Employee("David", "Davitidze", "Software Engineer", "IT", new DateOnly(1990, 5, 15).ToDateTime(TimeOnly.MinValue));
        var nino = new Employee("Nino", "Ninuladze", "Administrator", "HR", new DateOnly(1985, 8, 25).ToDateTime(TimeOnly.MinValue));

        var identityUsers = new List<IdentityUser>
            {
                new IdentityUser
                {
                    Id = david.Id.ToString(),
                    UserName = "david",
                    Email = "david@example.com"
                },
                new IdentityUser
                {
                    Id = nino.Id.ToString(),
                    UserName = "nino",
                    Email = "nino@example.com"
                }
            };

        dbContext.Employees.AddRange(david, nino);
        await dbContext.SaveChangesAsync();

        foreach (var user in identityUsers)
        {
            var result = await userManager.CreateAsync(user, user.UserName == "david" ? "david123" : "nino123");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, user.UserName == "david" ? "Employee" : "Administrator");
            }
        }
    }

    private static async Task SeedNotificationsAsync(DomainDbContext dbContext)
    {
        if (await dbContext.Notifications.AnyAsync()) return;

        var notifications = new List<Notification>
            {
                new("System Maintenance", "The system will be down for maintenance at 2 AM."),
                new("New Feature Release", "A new feature has been added to the application."),
                new("Security Alert", "Please update your password due to a recent security update.")
            };

        dbContext.Notifications.AddRange(notifications);
        await dbContext.SaveChangesAsync();

        var employees = await dbContext.Employees.ToListAsync();
        var employeeNotifications = new List<EmployeeNotification>();

        foreach (var employee in employees)
        {
            foreach (var notification in notifications)
            {
                employeeNotifications.Add(new EmployeeNotification(employee, notification));
            }
        }

        dbContext.EmployeeNotifications.AddRange(employeeNotifications);
        await dbContext.SaveChangesAsync();
    }
}