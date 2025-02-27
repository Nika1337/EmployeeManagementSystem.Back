using Microsoft.AspNetCore.Authentication.JwtBearer;
using Presentation;
using Infrastructure;
using Infrastructure.Data;
using FastEndpoints.Swagger;
using FastEndpoints.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddAuthenticationJwtBearer(s => s.SigningKey = AuthorizationExtensions.SecretKey);
builder.Services.AddAuthentication(o =>
    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);

builder.Services.AddAuthorization();
builder.Services.AddPresentationLayer();

builder.Services.SwaggerDocument();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await DataSeeder.SeedAsync(serviceProvider);
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UsePresentationLayer();
app.UseSwaggerGen();

app.Run();
