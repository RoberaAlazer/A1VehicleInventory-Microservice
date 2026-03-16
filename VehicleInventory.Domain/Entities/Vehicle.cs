using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Domain.Events;
using VehicleInventory.Domain.ValueObjects;
namespace VehicleInventory.Domain.Entities;
public class Vehicle
{
    private readonly List<IDomainEvent> _domainEvents = new();
    public int Id { get; private set; }
    public VehicleCode VehicleCode { get; private set; }
    public VehicleType VehicleType { get; private set; }
    public int LocationId { get; private set; }
    public VehicleStatus Status { get; private set; }

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Vehicle()
    {
        VehicleCode = null!;
        VehicleType = null!;
    }

    public Vehicle(string vehicleCode, int locationId, string vehicleType)
    {
        if (locationId <= 0)
            throw new DomainException("LocationId must be greater than zero.");

        VehicleCode = new VehicleCode(vehicleCode);
        VehicleType = new VehicleType(vehicleType);
        LocationId = locationId;
        Status = VehicleStatus.Available;

        _domainEvents.Add(new VehicleCreatedEvent(Id, VehicleCode.Value, DateTime.UtcNow));
    }
    public static Vehicle Reconstitute(int id, string vehicleCode, int locationId, string vehicleType, VehicleStatus status)
    {
        var vehicle = new Vehicle
        {
            Id = id,
            VehicleCode = new VehicleCode(vehicleCode),
            VehicleType = new VehicleType(vehicleType),
            LocationId = locationId,
            Status = status
        };
        return vehicle;
    }
    public void MarkAvailable()
    {
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("A reserved vehicle cannot be marked as available. Use ReleaseReservation.");
        var old = Status.ToString();
        Status = VehicleStatus.Available;
        _domainEvents.Add(new VehicleStatusChangedEvent(Id, old, Status.ToString(), DateTime.UtcNow));
    }
    public void MarkReserved()
    {
        if (Status != VehicleStatus.Available)
            throw new DomainException("Only available vehicles can be reserved.");
        var old = Status.ToString();
        Status = VehicleStatus.Reserved;
        _domainEvents.Add(new VehicleStatusChangedEvent(Id, old, Status.ToString(), DateTime.UtcNow));
    }
    public void MarkRented()
    {
        if (Status != VehicleStatus.Available)
            throw new DomainException("Only available vehicles can be rented.");
        var old = Status.ToString();
        Status = VehicleStatus.Rented;
        _domainEvents.Add(new VehicleStatusChangedEvent(Id, old, Status.ToString(), DateTime.UtcNow));
    }
    public void MarkServiced()
    {
        if (Status == VehicleStatus.Rented)
            throw new DomainException("A rented vehicle cannot be sent for service.");
        var old = Status.ToString();
        Status = VehicleStatus.Serviced;
        _domainEvents.Add(new VehicleStatusChangedEvent(Id, old, Status.ToString(), DateTime.UtcNow));
    }
    public void ReleaseReservation()
    {
        if (Status != VehicleStatus.Reserved)
            throw new DomainException("Vehicle is not currently reserved.");
        var old = Status.ToString();
        Status = VehicleStatus.Available;
        _domainEvents.Add(new VehicleStatusChangedEvent(Id, old, Status.ToString(), DateTime.UtcNow));
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}

