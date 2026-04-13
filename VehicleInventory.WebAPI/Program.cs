using Microsoft.EntityFrameworkCore;
using VehicleInventory.Infrastructure.Data;
using VehicleInventory.Application.Services;
using VehicleInventory.Infrastructure;
using CarRental.SharedKernel.Middleware;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RAVehicleInventoryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VehicleInventoryDb")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<VehicleService>();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
