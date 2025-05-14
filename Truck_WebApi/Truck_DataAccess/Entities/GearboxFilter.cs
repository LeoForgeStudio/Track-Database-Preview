using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck_DataAccess.Entities
{
    public class GearboxFilter
    {
        public int? MaxTorque { get; set; }
        public int? MaxSpeed { get; set; }
        public int? MaxTemp { get; set; }
    }
}
