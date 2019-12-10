using GamingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamingApi.ViewModels
{
    public class ReviewViewModel
    {
        public string Name { get; set; }

        public double CriticsRating { get; set; }

        public int CriticsRatingCount { get; set; }

        public List<string> GameModes { get; set; }

        public List<string> Genres { get; set; }

        public List<string> Platforms { get; set; }

        public double UserRating { get; set; }

        public int UserRatingCount { get; set; }

        public double TotalRating { get; set; }

        public int TotalRatingCount { get; set; }

        public List<DateTime> ReleaseDates { get; set; }

        public string Summary { get; set; }

        public string GameUrl { get; set; }

        public ReviewViewModel()
        {

        }

        public ReviewViewModel(IgdbGame model)
        {
            CriticsRating = model.AggregatedRating;
            CriticsRatingCount = model.AggregatedRatingCount;
            GameModes = model.GameModes?.Select(x => x.Name).ToList() ?? new List<string>();
            Genres = model.Genres?.Select(x => x.Name).ToList() ?? new List<string>();
            Platforms = model.Platforms?.Select(x => x.Name).ToList() ?? new List<string>();
            UserRating = model.Rating;
            UserRatingCount = model.RatingCount;
            TotalRating = model.TotalRating;
            TotalRatingCount = model.TotalRatingCount;
            ReleaseDates = model.ReleaseDates?.Select(x => DateTimeOffset.FromUnixTimeSeconds(x.Date).UtcDateTime).ToList() ?? new List<DateTime>();
            Summary = model.Summary;
            GameUrl = model.Url;
        }
    }
}
