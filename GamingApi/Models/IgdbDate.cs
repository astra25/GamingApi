using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class IgdbDate
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("human")]
        public string Date { get; set; }

        [JsonPropertyName("platform")]
        public IgdbName Platform { get; set; }
    }
}
