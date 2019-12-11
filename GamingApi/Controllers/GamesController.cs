using GamingApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace GamingApi.Controllers
{
    public class GamesController : Controller
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IGamesService _gamesService;

        public GamesController(ILogger<GamesController> logger, IGamesService gamesService)
        {
            _logger = logger;
            _gamesService = gamesService;
        }

        // GET: HighestRatedGames
        public async Task<IActionResult> HighestRatedGames()
        {
            try
            {
                var popularGames = await _gamesService.GetHighestRatedGamesAsync();
                return View(popularGames);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in HighestRatedGames with message: {ex.Message}");
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
        }

        // GET: TopGames
        public async Task<IActionResult> TopGames()
        {
            try
            {
                var topGames = await _gamesService.GetTopGamesAsync();
                return View(topGames);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in TopGames with message: {ex.Message}");
                return RedirectToAction("Error", "Home", new { errorMessage = ex.Message });
            }
        }
    }
}