using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace VehicleInventory.Infrastructure.Data;
public class InventoryRow
{
    public int Id { get; set; }
    public int VehicleId { get; set; }
    public VehicleRow? Vehicle { get; set; }
    public int VehicleLocationId { get; set; }
    public VehicleLocationRow? VehicleLocation { get; set; }
    public int VehicleStatusId { get; set; }
    public VehicleStatusRow? VehicleStatus { get; set; }
    public DateTime LastUpdated { get; set; }
}
