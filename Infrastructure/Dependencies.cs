﻿
namespace Infrastructure;

using Application.Abstractions;
using Domain.Abstractions;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;


public static class Dependencies
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.ConfigureIdentity();
        services.AddRepositories();
        services.AddServices();
    }

    private static void AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DomainDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DomainConnection")));

        services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
    }

    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 2;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();

    }


    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeAuthenticationService, IdentityEmployeeAuthenticationService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<INotificationService, NotificationService>();
    }
}
