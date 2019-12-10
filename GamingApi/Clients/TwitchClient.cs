using GamingApi.Interfaces;
using GamingApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamingApi.Clients
{
    public class TwitchClient : ITwitchClient
    {
        private readonly HttpClient _httpClient;
        private ILogger<TwitchClient> _logger;
        
        public TwitchClient(HttpClient httpClient, ILogger<TwitchClient> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;

            _httpClient.BaseAddress = new Uri("https://api.twitch.tv");
            _httpClient.DefaultRequestHeaders.Add("Client-ID", config["TwitchClientID"]);
        }

        public async Task<TwitchResponse<TwitchGame>> GetTopGamesAsync()
        {
            try
            {
                var feedUrl = new Uri($"/helix/games/top?first=10", UriKind.Relative);
                var response = await _httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TwitchResponse<TwitchGame>>(responseStream);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in GetTopGamesAsync with message: {ex.Message}");
                throw;
            }
        }

        public async Task<TwitchResponse<TwitchGame>> GetGamesByNameAsync(string name)
        {
            try
            {
                var feedUrl = new Uri($"/helix/games/?name={name}", UriKind.Relative);
                var response = await _httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TwitchResponse<TwitchGame>>(responseStream);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in GetGamesByNameAsync with message: {ex.Message}");
                throw;
            }
        }

        public async Task<TwitchResponse<TwitchStream>> GetStreamsByIdAsync(int id)
        {
            try
            {
                var feedUrl = new Uri($"/helix/streams/?first=10&language=en&language=nl&game_id={id}", UriKind.Relative);
                var response = await _httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TwitchResponse<TwitchStream>>(responseStream);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in GetStreamsByIdAsync with message:{ex.Message}");
                throw;
            }
        }
    }
}
