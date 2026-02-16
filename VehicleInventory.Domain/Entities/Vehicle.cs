using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; private set; }
        public string VehicleCode { get; private set; }
        public string VehicleType { get; private set; }
        public int LocationId { get; private set; }
        public VehicleStatus Status { get; private set; }

        private Vehicle() { }

        public Vehicle(string vehicleCode, int locationId, string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new DomainException("VehicleCode is required.");
            if (locationId <= 0)
                throw new DomainException("LocationId must be positive.");
            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new DomainException("VehicleType is required.");

            VehicleCode = vehicleCode.Trim();
            LocationId = locationId;
            VehicleType = vehicleType.Trim();
            Status = VehicleStatus.Available;
        }

        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Available)
                throw new DomainException("Reserved cars cannot be mark as Available.");
            Status = VehicleStatus.Available;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Available cars cannot be mark as Reserved.");
            if (Status  == VehicleStatus.Serviced)
                throw new DomainException("Serviced cars cannot be mark as under Serviced.");
            Status = VehicleStatus.Reserved;
        }
         public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("vehicle cannot be rented if it is already rented.");
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("vehicle cannot be rented if it is reserved.");
            if (Status == VehicleStatus.Serviced)
                throw new DomainException("vehicle cannot be rented if it is under service.");

            Status = VehicleStatus.Rented;
        }
        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("rented vehicle cannot be marked as serviced.");

            Status = VehicleStatus.Serviced;
        }
        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainException("vehicle is not reserved.");

            Status = VehicleStatus.Available;
        }


    }
}
