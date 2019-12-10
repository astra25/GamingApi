using GamingApi.Models;
using System.Threading.Tasks;

namespace GamingApi.Interfaces
{
    public interface ITwitchClient
    {
        Task<TwitchResponse<TwitchGame>> GetTopGamesAsync();
        Task<TwitchResponse<TwitchGame>> GetGamesByNameAsync(string name);
        Task<TwitchResponse<TwitchStream>> GetStreamsByIdAsync(int id);
    }
}
