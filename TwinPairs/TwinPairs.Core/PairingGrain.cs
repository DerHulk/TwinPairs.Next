using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using TwinPairs.Interfaces;

namespace TwinPairs.Core
{
    /// <summary>
    /// Orleans grain implementation class GameGrain
    /// </summary>
    [Reentrant]
    public class PairingGrain : Grain, IPairingGrain
    {
        MemoryCache cache;

        public override Task OnActivateAsync()
        {
            cache = new MemoryCache("pairing");
            return base.OnActivateAsync();
        }

        public Task AddGame(Guid gameId, string name)
        {
            cache.Add(gameId.ToString(), name, new DateTimeOffset(DateTime.UtcNow).AddHours(1));
            return Task.CompletedTask;
        }

        public Task RemoveGame(Guid gameId)
        {
            cache.Remove(gameId.ToString());
            return Task.CompletedTask;
        }

        public Task<PairingSummary[]> GetGames()
        {
            return Task.FromResult(this.cache.Select(x => new PairingSummary { GameId = Guid.Parse(x.Key), Name = x.Value as string }).ToArray());
        }

    }
}
