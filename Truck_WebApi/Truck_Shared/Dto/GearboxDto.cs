namespace Truck_Shared.Dto
{
    public class GearboxDto : BaseEntityDto
    {
        public string Ratio { get; set; }
        public int MaxTorque { get; set; }
        public int MaxSpeed { get; set; }
        public int MaxTemp { get; set; }
    }
}
