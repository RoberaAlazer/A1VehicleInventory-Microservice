using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Domain.Events;
public sealed record VehicleCreatedEvent(int VehicleId, string VehicleCode, DateTime OccurredOn) : IDomainEvent;
public sealed record VehicleStatusChangedEvent(int VehicleId, string OldStatus, string NewStatus, DateTime OccurredOn) : IDomainEvent;

