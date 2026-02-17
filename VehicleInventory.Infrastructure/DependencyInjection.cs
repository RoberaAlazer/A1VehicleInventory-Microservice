using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Infrastructure.Data;
using VehicleInventory.Infrastructure.Repositories;

namespace VehicleInventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<VehicleInventoryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("VehicleInventoryDb")));

        services.AddScoped<IVehicleRepository, VehicleRepository>();

        return services;
    }
}
