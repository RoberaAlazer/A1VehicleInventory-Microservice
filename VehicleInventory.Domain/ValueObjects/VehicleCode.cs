using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;
namespace VehicleInventory.Domain.ValueObjects;
public sealed class VehicleCode : IEquatable<VehicleCode>
{
    public string Value { get; }

    public VehicleCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("VehicleCode cannot be empty.");
        if (value.Length > 50)
            throw new DomainException("VehicleCode cannot exceed 50 characters.");
        Value = value.Trim().ToUpperInvariant();
    }
    public bool Equals(VehicleCode? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is VehicleCode other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
}
