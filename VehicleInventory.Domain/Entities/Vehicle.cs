using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

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
                throw new DomainException("vehicleCode is required.");
            if (locationId <= 0)
                throw new DomainException("locationId must be positive.");
            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new DomainException("vehicleType is required.");

            VehicleCode = vehicleCode.Trim();
            LocationId = locationId;
            VehicleType = vehicleType.Trim();
            Status = VehicleStatus.Available;
        }
        public Vehicle(int id, string vehicleCode, int locationId, string vehicleType, VehicleStatus status)
        {
            Id = id;
            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = status;
        }
        public void MarkAvailable()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("reserved vehicle cannot be marked available without release.");
            Status = VehicleStatus.Available;
        }
        public void MarkReserved()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("rented vehicle cannot be reserved.");
            if (Status == VehicleStatus.Serviced)
                throw new DomainException("serviced vehicle cannot be reserved.");
            Status = VehicleStatus.Reserved;
        }
        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("vehicle is already rented.");
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("vehicle cannot be rented while reserved.");
            if (Status == VehicleStatus.Serviced)
                throw new DomainException("vehicle cannot be rented while under service.");
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
