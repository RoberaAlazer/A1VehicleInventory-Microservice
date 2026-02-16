using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(int id);
        Task<List<Vehicle>> GetAll();
        Task<int> CreateAsync (Vehicle vehicle);

        Task UpdateAsync(Vehicle vehicle);

        Task DeleteAsync(int id);
    }
}
