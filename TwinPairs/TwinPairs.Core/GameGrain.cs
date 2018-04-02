using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwinPairs.Interfaces;

namespace TwinPairs.Core
{
    [Reentrant]
    public class GameGrain : Grain, IGameGrain
    {
        public Task<GameState> AddPlayerToGame(Guid player)
        {
            throw new NotImplementedException();
        }

        public Task<List<GameMove>> GetMoves()
        {
            throw new NotImplementedException();
        }

        public Task<GameState> GetState()
        {
            throw new NotImplementedException();
        }

        public Task<GameSummary> GetSummary(Guid player)
        {
            throw new NotImplementedException();
        }

        public Task<GameState> MakeMove(GameMove move)
        {
            throw new NotImplementedException();
        }

        public Task SetName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
