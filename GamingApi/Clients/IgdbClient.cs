using GamingApi.Interfaces;
using GamingApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GamingApi.Clients
{
    public class IgdbClient : IIgdbClient
    {
        private readonly HttpClient _httpClient;
        private ILogger<IgdbClient> _logger;

        public IgdbClient(HttpClient httpClient, ILogger<IgdbClient> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;

            _httpClient.BaseAddress = new Uri("https://api-v3.igdb.com");
            _httpClient.DefaultRequestHeaders.Add("user-key", config["IgdbUserKey"]);
        }

        public async Task<List<IgdbGame>> GetPopularGamesAsync()
        {
            try
            {
                var feedUrl = new Uri($"/games", UriKind.Relative);

                var bodyText = @"fields id, aggregated_rating, aggregated_rating_count, alternative_names.name, category, cover.image_id, 
                                first_release_date, game_modes.name, genres.name, name, platforms.name, popularity, rating, rating_count, 
                                release_dates.date, slug, storyline, summary, total_rating, total_rating_count, url;
                                sort popularity desc;
                                where platforms = (6,48,49,130) & category = 0 & total_rating > 75 & themes != (42);";

                var body = new StringContent(bodyText, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(feedUrl, body);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<IgdbGame>>(responseStream);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in GetPopularGamesAsync with message: {ex.Message}");
                throw;
            }
        }

        public async Task<List<IgdbGame>> GetGameByNameAsync(string name)
        {
            try
            {
                var feedUrl = new Uri($"/games", UriKind.Relative);

                var bodyText = @"fields id, aggregated_rating, aggregated_rating_count, alternative_names.name, category, cover.image_id, 
                                first_release_date, game_modes.name, genres.name, name, platforms.name, popularity, rating, rating_count, 
                                release_dates.date, slug, storyline, summary, total_rating, total_rating_count, url;
                                where name ~ " + $"\"{name}\";";

                var body = new StringContent(bodyText, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(feedUrl, body);
                response.EnsureSuccessStatusCode();
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<IgdbGame>>(responseStream);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"Error in GetGameByNameAsync with message: {ex.Message}");
                throw;
            }
        }
    }
}
