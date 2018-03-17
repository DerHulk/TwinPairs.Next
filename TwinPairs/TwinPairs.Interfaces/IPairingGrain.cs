using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TwinPairs.Interfaces
{
    public interface IPairingGrain : IGrainWithIntegerKey
    {
        Task AddGame(Guid gameId, string name);
        Task RemoveGame(Guid gameId);
        Task<PairingSummary[]> GetGames();
    }

    public class PairingSummary
    {
        public Guid GameId { get; set; }
        public string Name { get; set; }
    }
}
