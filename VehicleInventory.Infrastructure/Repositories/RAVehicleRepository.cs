using Microsoft.EntityFrameworkCore;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.Infrastructure.Repositories;
public class RAVehicleRepository : RAIVehicleRepository
{
    private readonly RAVehicleInventoryDbContext _db;

    public RAVehicleRepository(RAVehicleInventoryDbContext db) => _db = db;

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        var inv = await _db.Inventory
            .Include(x => x.Vehicle)
            .ThenInclude(v => v!.VehicleType)
            .FirstOrDefaultAsync(x => x.VehicleId == id);

        if (inv?.Vehicle == null) return null;

        var status = (VehicleStatus)inv.VehicleStatusId;
        var code = $"{inv.Vehicle.Make}-{inv.Vehicle.Model}-{inv.Vehicle.Id}";
        var type = inv.Vehicle.VehicleType?.Name ?? "Unknown";

        return Vehicle.Reconstitute(inv.Vehicle.Id, code, inv.VehicleLocationId, type, status);
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        var list = await _db.Inventory
            .Include(x => x.Vehicle)
            .ThenInclude(v => v!.VehicleType)
            .ToListAsync();

        var result = new List<Vehicle>();

        foreach (var inv in list)
        {
            if (inv.Vehicle == null) continue;

            var status = (VehicleStatus)inv.VehicleStatusId;
            var code = $"{inv.Vehicle.Make}-{inv.Vehicle.Model}-{inv.Vehicle.Id}";
            var type = inv.Vehicle.VehicleType?.Name ?? "Unknown";

            result.Add(Vehicle.Reconstitute(inv.Vehicle.Id, code, inv.VehicleLocationId, type, status));
        }
        return result;
    }
    public async Task<int> CreateAsync(Vehicle vehicle)
    {
        var make = vehicle.VehicleCode.Value;
        var model = "Model";

        if (vehicle.VehicleCode.Value.Contains('-'))
        {
            var parts = vehicle.VehicleCode.Value.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                make = parts[0].Trim();
                model = parts[1].Trim();
            }
        }
        var type = await _db.VehicleTypes.FirstOrDefaultAsync(x => x.Name == vehicle.VehicleType.Value);
        if (type == null)
        {
            type = new RAVehicleTypeRow { Name = vehicle.VehicleType.Value };
            _db.VehicleTypes.Add(type);
            await _db.SaveChangesAsync();
        }
        var row = new RAVehicleRow { Make = make, Model = model, VehicleTypeId = type.Id };
        _db.Vehicles.Add(row);
        await _db.SaveChangesAsync();

        var inv = new RAInventoryRow
        {
            VehicleId = row.Id,
            VehicleLocationId = vehicle.LocationId,
            VehicleStatusId = (int)vehicle.Status,
            LastUpdated = DateTime.UtcNow
        };
        _db.Inventory.Add(inv);
        await _db.SaveChangesAsync();

        vehicle.ClearDomainEvents();
        return row.Id;
    }
    public async Task UpdateAsync(Vehicle vehicle)
    {
        var inv = await _db.Inventory.FirstOrDefaultAsync(x => x.VehicleId == vehicle.Id);
        if (inv == null) throw new DomainException("Inventory record not found.");

        inv.VehicleLocationId = vehicle.LocationId;
        inv.VehicleStatusId = (int)vehicle.Status;
        inv.LastUpdated = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        vehicle.ClearDomainEvents();
    }

    public async Task DeleteAsync(int id)
    {
        var inv = await _db.Inventory.FirstOrDefaultAsync(x => x.VehicleId == id);
        if (inv != null) _db.Inventory.Remove(inv);

        var vehicle = await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicle != null) _db.Vehicles.Remove(vehicle);

        await _db.SaveChangesAsync();
    }
}