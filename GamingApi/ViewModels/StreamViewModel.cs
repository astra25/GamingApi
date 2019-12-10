using GamingApi.Models;
using System;

namespace GamingApi.ViewModels
{
    public class StreamViewModel
    {
        public string UserName { get; set; }

        public string Type { get; set; }

        public string Title { get; set; }

        public int ViewerCount { get; set; }

        public DateTime StartedAt { get; set; }

        public string Language { get; set; }

        public StreamViewModel()
        {

        }

        public StreamViewModel(TwitchStream model)
        {
            UserName = model.UserName;
            Type = model.Type;
            Title = model.Title;
            ViewerCount = model.ViewerCount;
            StartedAt = model.StartedAt;
            Language = model.Language;
        }
    }
}
