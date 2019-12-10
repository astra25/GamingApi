using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class IgdbImage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("image_id")]
        public string ImageId { get; set; }
    }
}
