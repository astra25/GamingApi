using GamingApi.Interfaces;
using GamingApi.ViewModels;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamingApi.Services
{
    public class GamesService : IGamesService
    {
        private readonly IIgdbClient _igdbClient;
        private readonly ITwitchClient _twitchClient;
        private ILogger<GamesService> _logger;
        private IMemoryCache _cache;

        public GamesService(IIgdbClient igdbClient, ITwitchClient twitchClient, ILogger<GamesService> logger, IMemoryCache cache)
        {
            _igdbClient = igdbClient;
            _twitchClient = twitchClient;
            _logger = logger;
            _cache = cache;
        }

        public async Task<List<GameViewModel>> GetHighestRatedGamesAsync()
        {
            try
            {
                var cacheEntry = await _cache.GetOrCreateAsync("popular-games", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    return await GetLiveHighestRatedGamesAsync();
                });

                return cacheEntry;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in GetPopularGamesAsync with message: {ex.Message}");
                throw;
            }
        }

        public async Task<List<GameViewModel>> GetTopGamesAsync()
        {
            try
            {
                var cacheEntry = await _cache.GetOrCreateAsync("top-games", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60);
                    return await GetLiveTopGamesAsync();
                });

                return cacheEntry;
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error in GetTopGamesAsync with message:{ex.Message}");
                throw;
            }
        }

        private async Task<List<GameViewModel>> GetLiveHighestRatedGamesAsync()
        {
            var popularGamesView = new List<GameViewModel>();
            var popularGames = await _igdbClient.GetHighestRatedGamesAsync();

            if (popularGames != null && popularGames.Any())
            {
                await popularGames.ForEachAsync(10, async game =>
                {
                    var coverArt = $"//images.igdb.com/igdb/image/upload/t_cover_big/{game.Cover?.ImageId}.jpg";
                    var gameViewModel = new GameViewModel(game.Name, coverArt);
                    gameViewModel.Review = new ReviewViewModel(game);

                    var twitchGames = await _twitchClient.GetGamesByNameAsync(game.Name);

                    if (twitchGames.Data != null && twitchGames.Data.Any())
                    {
                        var firstMatch = twitchGames.Data.First();
                        var id = Int32.Parse(firstMatch.Id);
                        var streams = await _twitchClient.GetStreamsByIdAsync(id);

                        if (streams.Data != null && streams.Data.Any())
                        {
                            var streamsViewModel = new List<StreamViewModel>();
                            streamsViewModel.AddRange(streams.Data.Select(x => new StreamViewModel(x)));
                            gameViewModel.Streams = streamsViewModel;
                        }
                    }

                    popularGamesView.Add(gameViewModel);
                });
            }

            return popularGamesView;
        }

        private async Task<List<GameViewModel>> GetLiveTopGamesAsync()
        {
            var topGamesView = new List<GameViewModel>();
            var topGames = await _twitchClient.GetTopGamesAsync();

            if (topGames != null && topGames.Data.Any())
            {
                await topGames.Data.ForEachAsync(10, async game =>
                {
                    var coverArt = game.BoxArtUrl.Replace("{width}x{height}", "285x380");
                    var gameViewModel = new GameViewModel(game.Name, coverArt);

                    var id = Int32.Parse(game.Id);
                    var streams = await _twitchClient.GetStreamsByIdAsync(id);

                    if (streams.Data != null && streams.Data.Any())
                    {
                        var streamsViewModel = new List<StreamViewModel>();
                        streamsViewModel.AddRange(streams.Data.Select(x => new StreamViewModel(x)));
                        gameViewModel.Streams = streamsViewModel;
                    }

                    var igdbGames = await _igdbClient.GetGameByNameAsync(game.Name);

                    if (igdbGames != null && igdbGames.Any())
                    {
                        var firstMatch = igdbGames.First();
                        gameViewModel.Review = new ReviewViewModel(firstMatch);
                    }

                    topGamesView.Add(gameViewModel);
                });
            }

            return topGamesView;
        }
    }
}
