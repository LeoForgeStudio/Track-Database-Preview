

namespace Truck_DataAccess.Entities
{
    public class Gearbox : BaseEntity
    {
       
        public string Ratio { get; set; }
        public string MaxTorque { get; set; }
        public string MaxSpeed { get; set; }
        public string MaxTemp { get; set; }
    }
}
