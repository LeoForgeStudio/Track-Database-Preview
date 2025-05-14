

namespace Truck_DataAccess.Entities
{
    public class Gearbox : BaseEntity
    {
       
        public string Ratio { get; set; }
        public int MaxTorque { get; set; }
        public int MaxSpeed { get; set; }
        public int MaxTemp { get; set; }
    }
}
