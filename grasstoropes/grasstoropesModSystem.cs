using grasstoropes.ModConfiguration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace grasstoropes
{
    public class grasstoropesModSystem : ModSystem
    {
        public static ModConfig ModConfig = null!;
        public override void Start(ICoreAPI api)
        {
            ModConfig = api.LoadModConfig<ModConfig>("grasstoropes.json");

            if (ModConfig == null)
            {
                ModConfig = new ModConfig();
                api.StoreModConfig(ModConfig, "grasstoropes.json");
            }
            api.Logger.Event("started 'Grass To Ropes' mod");
        }
        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);
            handleFlax(api);
        }
        public override void StartServerSide(ICoreServerAPI api)
        {
            base.StartServerSide(api);
            handleFlax(api);
        }
        private void handleFlax(ICoreAPI api)
        {
            var recipeCode = new AssetLocation("grasstoropes:recipes/grid/flaxfibers.json");
            if (!ModConfig.flaxEnabled)
            {
                api.Logger.Notification("[Grass To Ropes] Disabling Bundle to Flax Fiber recipe per config.");
                var recipe = api.World.GridRecipes.FirstOrDefault(r => r.Name.Equals(recipeCode));
                if (recipe != null)
                {
                    recipe.Enabled = false;
                    api.Logger.Notification($"[Grass To Ropes] Recipe {recipeCode} has been disabled.");
                }
                else
                {
                    api.Logger.Warning($"[Grass To Ropes] Couldn't find a grid recipe named {recipeCode} to disable.");
                }
            }
            else
            {
                api.Logger.Notification("[Grass To Ropes] Bundle to Flax Fiber recipe enabled per config.");
            }
        }
    }
}
