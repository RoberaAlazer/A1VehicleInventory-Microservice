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
                throw new DomainException("Reserved vehicle cannot be marked as available without release.");
            Status = VehicleStatus.Available;
        }

        public void MarkReserved()
        {
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle is already reserved.");
            if (Status == VehicleStatus.Serviced)
                throw new DomainException("Serviced vehicle cannot be reserved.");
            Status = VehicleStatus.Reserved;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Vehicle cannot be rented if it is already rented.");
            if (Status == VehicleStatus.Reserved)
                throw new DomainException("Vehicle cannot be rented if it is reserved.");
            if (Status == VehicleStatus.Serviced)
                throw new DomainException("Vehicle cannot be rented if it is under service.");
            Status = VehicleStatus.Rented;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new DomainException("Rented vehicle cannot be marked as serviced.");
            Status = VehicleStatus.Serviced;
        }

        public void ReleaseReservation()
        {
            if (Status != VehicleStatus.Reserved)
                throw new DomainException("Vehicle is not reserved.");
            Status = VehicleStatus.Available;
        }
    }
}
