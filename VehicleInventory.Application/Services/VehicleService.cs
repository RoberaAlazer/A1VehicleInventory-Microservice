using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Application.Services;

public class VehicleService
{
    private readonly RAIVehicleRepository _repo;

    public VehicleService(RAIVehicleRepository repo) => _repo = repo;

    public async Task<int> CreateVehicleAsync(RACreateVehicleRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.VehicleCode))
            throw new DomainException("VehicleCode is required.");

        if (req.LocationId <= 0)
            throw new DomainException("LocationId must be greater than 0.");

        if (string.IsNullOrWhiteSpace(req.VehicleType))
            throw new DomainException("VehicleType is required.");

        var vehicle = new Vehicle(req.VehicleCode.Trim(), req.LocationId, req.VehicleType.Trim());
        return await _repo.CreateAsync(vehicle);
    }

    public async Task<RAVehicleResponse?> GetVehicleByIdAsync(int id)
    {
        var v = await _repo.GetByIdAsync(id);
        return v == null ? null : ToResponse(v);
    }

    public async Task<List<RAVehicleResponse>> GetAllVehiclesAsync()
    {
        var list = await _repo.GetAllAsync();
        return list.Select(ToResponse).ToList();
    }

    public async Task UpdateVehicleStatusAsync(int id, int statusId)
    {
        var vehicle = await _repo.GetByIdAsync(id);
        if (vehicle == null)
            throw new DomainException("Vehicle not found.");

        var status = (VehicleStatus)statusId;

        switch (status)
        {
            case VehicleStatus.Available:
                vehicle.MarkAvailable();
                break;
            case VehicleStatus.Reserved:
                vehicle.MarkReserved();
                break;
            case VehicleStatus.Rented:
                vehicle.MarkRented();
                break;
            case VehicleStatus.Serviced:
                vehicle.MarkServiced();
                break;
            default:
                throw new DomainException("Invalid status.");
        }

        await _repo.UpdateAsync(vehicle);
    }

    public Task DeleteVehicleAsync(int id) => _repo.DeleteAsync(id);

    private static RAVehicleResponse ToResponse(Vehicle v) => new RAVehicleResponse
    {
        Id = v.Id,
        VehicleCode = v.VehicleCode,
        LocationId = v.LocationId,
        VehicleType = v.VehicleType,
       Status = v.Status
    };
}
