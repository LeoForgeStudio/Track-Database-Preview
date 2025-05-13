using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Truck_DataAccess.Enums;

namespace Truck_DataAccess.Entities
{
    public class Truck : BaseEntity
    {
        
        public Manufacturer Manufacturer { get; set; }
        public string Model { get; set; }
        public DateOnly ConstructDate { get; set; }
        public Condition Condition { get; set; } 
        public required TechnicalData TechnicalData { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
