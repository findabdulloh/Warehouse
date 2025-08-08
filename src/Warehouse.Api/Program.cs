using FleetFlow.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using Warehouse.Api.Extensions;
using Warehouse.Data.DbContexts;
using Warehouse.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// terminal's location should be in Warehouse.Api to create a migration
// dotnet ef --project ..\Warehouse.Data\ migrations add [MigrationName]
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCustomServices();

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MapperProfile>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.ApplyMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();