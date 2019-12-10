using System.Collections.Generic;

namespace GamingApi.ViewModels
{
    public class GameViewModel
    {
        public string Name { get; set; }

        public string CoverArt { get; set; }

        public ReviewViewModel Review { get; set; }

        public List<StreamViewModel> Streams { get; set; }

        public GameViewModel()
        {

        }

        public GameViewModel(string name, string coverArt)
        {
            Name = name;
            CoverArt = coverArt;
        }
    }
}
