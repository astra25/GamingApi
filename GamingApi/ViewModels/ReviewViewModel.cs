using GamingApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamingApi.ViewModels
{
    public class ReviewViewModel
    {
        public string Name { get; set; }

        public string CriticsRating { get; set; }

        public int CriticsRatingCount { get; set; }

        public List<string> GameModes { get; set; }

        public List<string> Genres { get; set; }

        public List<string> Platforms { get; set; }

        public string UserRating { get; set; }

        public int UserRatingCount { get; set; }

        public string TotalRating { get; set; }

        public int TotalRatingCount { get; set; }

        public List<ReleaseDate> ReleaseDates { get; set; }

        public string Summary { get; set; }

        public string GameUrl { get; set; }

        public ReviewViewModel()
        {

        }

        public ReviewViewModel(IgdbGame model)
        {
            CriticsRating = string.Format("{0:0.00}", model.AggregatedRating);
            CriticsRatingCount = model.AggregatedRatingCount;
            GameModes = model.GameModes?.Select(x => x.Name).ToList() ?? new List<string>();
            Genres = model.Genres?.Select(x => x.Name).ToList() ?? new List<string>();
            UserRating = string.Format("{0:0.00}", model.Rating);
            UserRatingCount = model.RatingCount;
            TotalRating = string.Format("{0:0.00}", model.TotalRating);
            TotalRatingCount = model.TotalRatingCount;
            ReleaseDates = model.ReleaseDates?.Select(x => new ReleaseDate { Date = x.Date, Platform = x.Platform?.Name }).ToList() ?? new List<ReleaseDate>();
            Summary = model.Summary;
            GameUrl = model.Url;
        }
    }

    public class ReleaseDate
    {
        public string Date { get; set; }
        public string Platform { get; set; }
    }
}
