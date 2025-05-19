using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck_DataAccess.Entities.Filters
{
    public class EngineFilter
    {
        public string? Model { get; set; }
        public int? Cilinders { get; set; }
        public int? MaxPower { get; set; }
        public int? MaxTorque { get; set; }
    }
}
