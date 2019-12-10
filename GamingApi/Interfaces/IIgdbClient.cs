using GamingApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingApi.Interfaces
{
    public interface IIgdbClient
    {
        Task<List<IgdbGame>> GetPopularGamesAsync();
        Task<List<IgdbGame>> GetGameByNameAsync(string name);
    }
}
