using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Truck_DataAccess.Enums;

namespace Truck_DataAccess.Entities
{
     public class TruckFilter
    {
        public Manufacturer? Manufacturer { get; set; }
        public string? Model { get; set; }
        public DateOnly? ConstructFrom { get; set; }
        public DateOnly? ConstructTo { get; set; }
        public Condition? Condition { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string? Location { get; set; }
        
    }
}
