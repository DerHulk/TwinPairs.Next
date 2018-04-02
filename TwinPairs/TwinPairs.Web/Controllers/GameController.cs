using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwinPairs.Interfaces;
using Orleans;
using System.Net;
using Orleans.Runtime;

namespace TwinPairs.Web.Controllers
{
    public class GameController :Controller
    {
        private IClusterClient Client { get; }

        public GameController(IClusterClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private Guid GetGuid()
        {
            if (this.Request.Cookies["playerId"] != null)
            {
                return Guid.Parse(this.Request.Cookies.SingleOrDefault(x=> x.Key == "playerId").Value);
            }
            var guid = Guid.NewGuid();
            this.Response.Cookies.Append("playerId", guid.ToString());
            return guid;
        }

        public async Task<ActionResult> Index()
        {            
            var guid = GetGuid();
            var player = this.Client.GetGrain<IPlayerGrain>(guid);
            var gamesTask = player.GetGameSummaries();
            var availableTask = player.GetAvailableGames();
            await Task.WhenAll(gamesTask, availableTask);

            return Json(new object[] { gamesTask.Result, availableTask.Result });
        }

        public async Task<ActionResult> CreateGame()
        {
            var guid = GetGuid();
            var player = this.Client.GetGrain<IPlayerGrain>(guid);
            var gameIdTask = await player.CreateGame();
            return Json(new { GameId = gameIdTask });
        }

        public async Task<ActionResult> Join(Guid id)
        {
            var guid = GetGuid();
            var player = this.Client.GetGrain<IPlayerGrain>(guid);
            var state = await player.JoinGame(id);
            return Json(new { GameState = state });
        }

        public async Task<ActionResult> GetMoves(Guid id)
        {
            var guid = GetGuid();
            var game = this.Client.GetGrain<IGameGrain>(id);
            var moves = await game.GetMoves();
            var summary = await game.GetSummary(guid);
            return Json(new { moves = moves, summary = summary });
        }

        [HttpPost]
        public async Task<ActionResult> MakeMove(Guid id, int x, int y)
        {
            var guid = GetGuid();
            var game = this.Client.GetGrain<IGameGrain>(id);
            var move = new GameMove { PlayerId = guid, X = x, Y = y };
            var state = await game.MakeMove(move);
            return Json(state);
        }

        public async Task<ActionResult> QueryGame(Guid id)
        {
            var game = this.Client.GetGrain<IGameGrain>(id);
            var state = await game.GetState();
            return Json(state);

        }

        [HttpPost]
        public async Task<ActionResult> SetUser(string id)
        {
            var guid = GetGuid();
            var player = this.Client.GetGrain<IPlayerGrain>(guid);
            await player.SetUsername(id);
            return Json(new { });
        }      

    }
}
