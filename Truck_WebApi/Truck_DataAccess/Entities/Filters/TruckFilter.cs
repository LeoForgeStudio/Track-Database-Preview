using Truck_Shared.Enums;
using Truck_Shared.Dto;

namespace Truck_DataAccess.Entities
{
     public class TruckFilter
    {
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public DateOnly? ConstructFrom { get; set; }
        public DateOnly? ConstructTo { get; set; }
        public Condition? Condition { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public string? Location { get; set; }
        
    }
}
