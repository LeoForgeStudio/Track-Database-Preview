using System.Text.Json.Serialization;

namespace Truck_Shared.Dto
{
    public class BaseEntityDto
    {
        [JsonPropertyOrder(0)]
        public string Id { get; set; }
        [JsonPropertyOrder(1)]
        public string Model { get; set; }
    }
}
