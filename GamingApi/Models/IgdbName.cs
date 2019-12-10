using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class IgdbName
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
