using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwinPairs.Interfaces;

namespace TwinPairs.Core
{
    [Reentrant]
    public class GameGrain : Grain, IGameGrain
    {
        private HashSet<Guid> Players { get; } = new HashSet<Guid>();
        private string Name { get; set; }
        private GameState State { get; set; }

        public Task<GameState> AddPlayerToGame(Guid player)
        {
            this.Players.Add(player);
            this.State = GameState.AwaitingPlayers;
            return Task.FromResult(this.State);
        }

        public Task SetName(string name)
        {
            this.Name = name;
            return Task.CompletedTask;
        }

        public Task<List<GameMove>> GetMoves()
        {
            throw new NotImplementedException();
        }

        public Task<GameState> GetState()
        {
            throw new NotImplementedException();
        }

        public async Task<GameSummary> GetSummary(Guid player)
        {
            var tasks = this.Players.Select(p => this.GrainFactory.GetGrain<IPlayerGrain>(p).GetUsername()).ToArray();
            var playerNames = await Task.WhenAll(tasks);

            var summary = new GameSummary()
            {
                GameId = this.GrainReference.GetPrimaryKey(),
                Name = this.Name,
                GameStarter = this.State == GameState.InPlay,
                NumPlayers = this.Players.Count,
                Usernames = playerNames,
                State = this.State,
                NumMoves = 0,
                YourMove = false,
                Outcome = GameOutcome.Draw,
            };

            return summary;
        }

        public Task<GameState> MakeMove(GameMove move)
        {
            throw new NotImplementedException();
        }

        
    }
}
