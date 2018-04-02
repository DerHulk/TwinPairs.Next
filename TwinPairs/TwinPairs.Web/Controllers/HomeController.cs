using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using TwinPairs.Interfaces;

namespace TwinPairs.Web.Controllers
{
    public class HomeController : Controller
    {
        private IClusterClient Client { get; }

        public HomeController(IClusterClient client)
        {
            this.Client = client ?? throw new ArgumentNullException(nameof(client));
        }

        private Guid GetGuid()
        {
            var key = "playerId";
            if (!this.HttpContext.Session.Keys.Any(x=> x == "playerId"))
            {
                byte[] raw = null;
                if(this.HttpContext.Session.TryGetValue("playerId", out raw))
                {
                    return new Guid(raw);
                }
                this.HttpContext.Session.Remove(key);                
            }
            var guid = Guid.NewGuid();
            this.HttpContext.Session.Set(key, guid.ToByteArray());
            
            return guid;
        }

        public class ViewModel
        {
            public string GameId { get; set; }
        }

        public ActionResult Index(Guid? id)
        {
            var vm = new ViewModel();
            vm.GameId = (id.HasValue) ? id.Value.ToString() : "";
            return View(vm);
        }

        public async Task<ActionResult> Join(Guid id)
        {
            var guid = GetGuid();
            var player = this.Client.GetGrain<IPlayerGrain>(guid);
            var state = await player.JoinGame(id);
            return RedirectToAction("Index", id);
        }
    }
}
