using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class TwitchResponse<T> where T : class
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        [JsonPropertyName("pagination")]
        public TwitchPagination Pagination { get; set; }
    }

    public class TwitchPagination
    {
        [JsonPropertyName("cursor")]
        public string Cursor { get; set; }
    }
}
