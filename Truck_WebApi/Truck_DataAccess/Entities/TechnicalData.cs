
using Truck_DataAccess.Enums;

namespace Truck_DataAccess.Entities
{
    public class TechnicalData
    {
        public Engine Engine { get; set; }
        public Gearbox Gearbox { get; set; }
        public string Weight { get; set; }
        public FuelType Name { get; set; }
        public string Color { get; set; }
        public int Axle { get; set; }
        public Dimentions Dimentions { get; set; }
        public int WheelBase { get; set; }
        public EmissionClass EmissionClass { get; set; }
    }
}
