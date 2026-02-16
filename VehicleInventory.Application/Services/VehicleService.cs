using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace VehicleInventory.Application.Services
{
    public class VehicleService
    {
        private readonly IVehicleRepository _repo;

        public VehicleService(IVehicleRepository repo) => _repo = repo;

        public async Task<int> CreateVehicleAsync(CreateVehicleRequest req)
        {
            if (req.LocationId <= 0)
                throw new DomainException("LocationId must be greater than 0.");

            var vehicle = new Vehicle(req.VehicleCode, req.LocationId, req.VehicleType);
            return await _repo.CreateAsync(vehicle);
        } 

        public Task<Vehicle?> GetVehicleByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task<List<Vehicle>> GetAllVehiclesAsync() => _repo.GetAllAsync();


        public async Task UpdateVehiclestatusAsync(int id, int statusId)

        {
            var vehicle = await _repo.GetByIdAsync(id);
            if (vehicle == null) throw new DomainException(" Vehicle not found.");

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
            public Task DeleteVehicleAsync (int id) => _repo.DeleteAsync(id);

    }
    }

