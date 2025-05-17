using System.Text.Json.Serialization;
using Truck_Shared.Enums;

namespace Truck_Shared.Dto
{
    public class TruckDto : BaseEntityDto
    {
        [JsonPropertyOrder(2)]
        public string Manufacturer { get; set; }

        [JsonPropertyOrder(3)]
        public DateOnly ConstructDate { get; set; }

        [JsonPropertyOrder(4)]
        public Condition Condition { get; set; }

        [JsonPropertyOrder(5)]
        public required TechnicalDataDto TechnicalData { get; set; }

        [JsonPropertyOrder(6)]
        public int Price { get; set; }

        [JsonPropertyOrder(7)]
        public string Location { get; set; }

        [JsonPropertyOrder(8)]
        public string Description { get; set; }
        
    }
}
