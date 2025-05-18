using Truck_Shared.Enums;
namespace Truck_Shared.Dto
{
    public class TechnicalDataDto
    {
        public string Engine { get; set; }
        public string Gearbox { get; set; }
        public int Weight { get; set; }
        public FuelType FuelType { get; set; }
        public string Color { get; set; }
        public int Axle { get; set; }
        public DimentionsDto Dimentions { get; set; }
        public int WheelBase { get; set; }
        public EmissionClass EmissionClass { get; set; }
    }
}
