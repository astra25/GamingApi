using System;
using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class IgdbDate
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public int Date { get; set; }
    }
}
