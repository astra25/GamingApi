﻿using GamingApi.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamingApi.Interfaces
{
    public interface IGamesService
    {
        Task<List<GameViewModel>> GetHighestRatedGamesAsync();
        Task<List<GameViewModel>> GetTopGamesAsync();
    }
}
