using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Infrastructure.Data;
public class RAVehicleRow
{
    public int Id { get; set; }
    public string Make { get; set; } = "";
    public string Model { get; set; } = "";
    public int VehicleTypeId { get; set; }
    public RAVehicleTypeRow? VehicleType { get; set; }
}
