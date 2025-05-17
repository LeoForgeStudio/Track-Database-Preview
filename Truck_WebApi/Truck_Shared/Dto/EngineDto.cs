

using System.Text.Json.Serialization;

namespace Truck_Shared.Dto
{
    public class EngineDto : BaseEntityDto
    {
        [JsonPropertyOrder(2)]
        public int Cilinders { get; set; }

        [JsonPropertyOrder(3)]
        public int Power { get; set; }

        [JsonPropertyOrder(4)]
        public int MaxTorque { get; set; }
    }
}
