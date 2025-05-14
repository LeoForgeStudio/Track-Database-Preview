using Truck_Shared.Enums;

namespace Truck_Shared.Dto
{
    public class TruckDto : BaseEntityDto
    {
        public string Manufacturer { get; set; }
        public DateOnly ConstructDate { get; set; }
        public Condition Condition { get; set; }
        public required TechnicalDataDto TechnicalData { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }
}
