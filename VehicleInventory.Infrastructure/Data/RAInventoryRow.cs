using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VehicleInventory.Infrastructure.Data;
public class RAInventoryRow
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public RAVehicleRow? Vehicle { get; set; }
    public int VehicleLocationId { get; set; }
    public RAVehicleLocationRow? VehicleLocation { get; set; }
    public int VehicleStatusId { get; set; }
    public RAVehicleStatusRow? VehicleStatus { get; set; }
    public DateTime LastUpdated { get; set; }
}
