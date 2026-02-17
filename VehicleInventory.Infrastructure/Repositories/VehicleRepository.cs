using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Infrastructure.Data;

namespace VehicleInventory.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly VehicleInventoryDbContext _db;
    public VehicleRepository(VehicleInventoryDbContext db) => _db = db;
    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        var inv = await _db.Inventory
            .Include(x => x.Vehicle)
            .ThenInclude(v => v!.VehicleType)
            .FirstOrDefaultAsync(x => x.VehicleId == id);
        if (inv?.Vehicle == null) return null;
        var domain = new Vehicle(
            vehicleCode: $"{inv.Vehicle.Make}-{inv.Vehicle.Model}-{inv.Vehicle.Id}",
            locationId: inv.VehicleLocationId,
            vehicleType: inv.Vehicle.VehicleType?.Name ?? "Unknown"
        );
        SetStatus(domain, inv.VehicleStatusId);
        SetId(domain, inv.Vehicle.Id);

        return domain;
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

            var domain = new Vehicle(
                vehicleCode: $"{inv.Vehicle.Make}-{inv.Vehicle.Model}-{inv.Vehicle.Id}",
                locationId: inv.VehicleLocationId,
                vehicleType: inv.Vehicle.VehicleType?.Name ?? "Unknown"
            );

            SetStatus(domain, inv.VehicleStatusId);
            SetId(domain, inv.Vehicle.Id);

            result.Add(domain);
        }

        return result;
    }
    public async Task<int> CreateAsync(Vehicle vehicle)
    {
        if (string.IsNullOrWhiteSpace(vehicle.VehicleCode))
            throw new DomainException("VehicleCode is required.");

        var make = vehicle.VehicleCode.Trim();
        var model = "Model";

        if (vehicle.VehicleCode.Contains('-'))
        {
            var parts = vehicle.VehicleCode.Split('-', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                make = parts[0].Trim();
                model = parts[1].Trim();
            }
        }
        var type = await _db.VehicleTypes.FirstOrDefaultAsync(x => x.Name == vehicle.VehicleType);
        if (type == null)
        {
            type = new VehicleTypeRow { Name = vehicle.VehicleType };
            _db.VehicleTypes.Add(type);
            await _db.SaveChangesAsync();
        }
        var row = new VehicleRow
        {
            Make = make,
            Model = model,
            VehicleTypeId = type.Id
        };
        _db.Vehicles.Add(row);
        await _db.SaveChangesAsync();
        var inv = new InventoryRow
        {
            VehicleId = row.Id,
            VehicleLocationId = vehicle.LocationId,
            VehicleStatusId = (int)vehicle.Status,
            LastUpdated = DateTime.UtcNow
        };

        _db.Inventory.Add(inv);
        await _db.SaveChangesAsync();
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
    }
    public async Task DeleteAsync(int id)
    {
        var inv = await _db.Inventory.FirstOrDefaultAsync(x => x.VehicleId == id);
        if (inv != null) _db.Inventory.Remove(inv);

        var vehicle = await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
        if (vehicle != null) _db.Vehicles.Remove(vehicle);

        await _db.SaveChangesAsync();
    }
    private static void SetStatus(Vehicle vehicle, int statusId)
    {
        var status = (VehicleStatus)statusId;

        if (status == VehicleStatus.Available) vehicle.MarkAvailable();
        else if (status == VehicleStatus.Reserved) vehicle.MarkReserved();
        else if (status == VehicleStatus.Rented) vehicle.MarkRented();
        else if (status == VehicleStatus.Serviced) vehicle.MarkServiced();
        else throw new DomainException("Invalid status in database.");
    }
    private static void SetId(Vehicle vehicle, int id)
    {
        var prop = typeof(Vehicle).GetProperty("Id");
        prop?.SetValue(vehicle, id);
    }
}
