using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamingApi.Models
{
    public class IgdbGame
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("aggregated_rating")]
        public double AggregatedRating { get; set; }

        [JsonPropertyName("aggregated_rating_count")]
        public int AggregatedRatingCount { get; set; }

        [JsonPropertyName("alternative_names")]
        public List<IgdbName> AlternativeNames { get; set; }

        [JsonPropertyName("category")]
        public int Category { get; set; }

        [JsonPropertyName("cover")]
        public IgdbImage Cover { get; set; }

        [JsonPropertyName("game_modes")]
        public List<IgdbName> GameModes { get; set; }

        [JsonPropertyName("genres")]
        public List<IgdbName> Genres { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("popularity")]
        public double Popularity { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("rating_count")]
        public int RatingCount { get; set; }

        [JsonPropertyName("release_dates")]
        public List<IgdbDate> ReleaseDates { get; set; }

        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("storyline")]
        public string Storyline { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("total_rating")]
        public double TotalRating { get; set; }

        [JsonPropertyName("total_rating_count")]
        public int TotalRatingCount { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
