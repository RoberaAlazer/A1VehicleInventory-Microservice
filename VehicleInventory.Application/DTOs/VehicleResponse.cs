using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleInventory.Application.DTOs
{
  public  class VehicleResponse
    {
        public int Id { get; set; }
        public string VehicleCode { get; set; } = "";
        public string VehicleType { get; set; } = "";
        public string Location { get; set; } = "";
        public string StatusId { get; set; } = "";
        public string StatusName { get; set; } = "";


    }
}
