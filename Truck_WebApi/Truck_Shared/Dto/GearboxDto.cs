using System.Text.Json.Serialization;

namespace Truck_Shared.Dto
{
    public class GearboxDto : BaseEntityDto
    {
        [JsonPropertyOrder(2)]
        public string Ratio { get; set; }

        [JsonPropertyOrder(3)]
        public int MaxTorque { get; set; }

        [JsonPropertyOrder(4)]
        public int MaxSpeed { get; set; }

        [JsonPropertyOrder(5)]
        public int MaxTemp { get; set; }
    }
}
