using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace grasstoropes {
    public class grasstoropesModSystem : ModSystem {
        public override void Start(ICoreAPI api) {
            api.Logger.Event("started 'Grass to Ropes' mod");
        }
    }
}
