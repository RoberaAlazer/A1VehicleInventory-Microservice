using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs;

public class VehicleResponse
{
    public int Id { get; set; }
    public string VehicleCode { get; set; } = "";
    public int LocationId { get; set; }
    public string VehicleType { get; set; } = "";
    public VehicleStatus Status { get; set; }
}
