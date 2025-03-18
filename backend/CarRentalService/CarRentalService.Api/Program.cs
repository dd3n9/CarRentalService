using CarRentalService.Api;
using CarRentalService.Infrastructure;
using CarRentalService.Infrastructure.EF.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPresentation()
    .AddInfrastructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
