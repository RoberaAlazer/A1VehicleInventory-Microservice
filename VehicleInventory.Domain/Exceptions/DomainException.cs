using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Exceptions;

public class DomainException : CarRental.SharedKernel.Exceptions.DomainException
{
    public DomainException(string message) : base(message) { }
}