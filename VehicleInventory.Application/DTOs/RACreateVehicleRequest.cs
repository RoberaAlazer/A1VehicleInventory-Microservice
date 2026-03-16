using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.DTOs
{
    public class RACreateVehicleRequest
    {
        public string VehicleCode { get; set; } = "";

        public int LocationId { get; set; }

        public string VehicleType { get; set; } = "";
    }
}
