using Vintagestory.API.Common;

namespace grasstoropes {
    public class grasstoropesModSystem : ModSystem {
        public override void Start(ICoreAPI api) {
            api.Logger.Event("started 'Grass to Ropes' mod");
        }
    }
}
