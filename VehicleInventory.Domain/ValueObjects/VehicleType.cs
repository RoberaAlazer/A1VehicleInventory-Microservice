using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleInventory.Domain.Exceptions;
namespace VehicleInventory.Domain.ValueObjects;
public sealed class VehicleType : IEquatable<VehicleType>
{
    public string Value { get; }

    public VehicleType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("VehicleType cannot be empty.");
        Value = value.Trim();
    }
    public bool Equals(VehicleType? other) => other is not null && string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
    public override bool Equals(object? obj) => obj is VehicleType other && Equals(other);
    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
    public override string ToString() => Value;
}
