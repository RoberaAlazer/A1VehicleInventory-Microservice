using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Infrastructure.Data.Row
{
    public class RAInventoryRow
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }

        public int VehicleLocationId { get; set; }
        public int VehicleStatusId { get; set; }
        public DateTime LastUpdated { get; set; }



    }
}
