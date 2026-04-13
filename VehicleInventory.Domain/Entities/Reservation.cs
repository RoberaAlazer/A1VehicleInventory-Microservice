using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities;

public class Reservation
{
    public int Id { get; private set; }
    public int CustomerId { get; private set; }
    public int VehicleId { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public string Status { get; private set; }

    private Reservation()
    {
        Status = string.Empty;
    }

    public Reservation(int customerId, int vehicleId, DateTime startDate, DateTime endDate)
    {
        if (customerId <= 0)
            throw new DomainException("CustomerId must be greater than zero.");
        if (vehicleId <= 0)
            throw new DomainException("VehicleId must be greater than zero.");
        if (endDate <= startDate)
            throw new DomainException("End date must be after start date.");

        CustomerId = customerId;
        VehicleId = vehicleId;
        StartDate = startDate;
        EndDate = endDate;
        Status = "Pending";
    }

    public void Confirm()
    {
        if (Status != "Pending")
            throw new DomainException("Only pending reservations can be confirmed.");
        Status = "Confirmed";
    }

    public void Cancel()
    {
        if (Status == "Cancelled")
            throw new DomainException("Reservation is already cancelled.");
        Status = "Cancelled";
    }
}