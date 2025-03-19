using CarRentalService.Api;
using CarRentalService.Application;
using CarRentalService.Infrastructure;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var versionDescProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var desc in versionDescProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", $"Car Rental - {desc.GroupName.ToUpper()}");
        }
    });
}

using (var serviceScope = app.Services.CreateScope())
{
    using var dbContext = serviceScope.ServiceProvider.GetService<WriteDbContext>();
    dbContext?.Database.Migrate();
};


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
