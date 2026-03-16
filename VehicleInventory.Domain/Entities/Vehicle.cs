using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities;

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
            throw new DomainException("locationId must be positive.");
        if (string.IsNullOrWhiteSpace(vehicleType))
            throw new DomainException("vehicleType is required.");
        VehicleCode = vehicleCode.Trim();
        LocationId = locationId;
        VehicleType = vehicleType.Trim();
        Status = VehicleStatus.Available;
    }

    public void MarkAvailable()
    {
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("reserved cars cannot be marked as Available.");
        Status = VehicleStatus.Available;
    }

    public void MarkReserved()
    {
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("serviced cars cannot be reserved.");
        if (Status == VehicleStatus.Rented)
            throw new DomainException("rented cars cannot be reserved.");
        Status = VehicleStatus.Reserved;
    }

    public void MarkRented()
    {
        if (Status != VehicleStatus.Available)
            throw new DomainException("only available vehicles can be rented.");
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
