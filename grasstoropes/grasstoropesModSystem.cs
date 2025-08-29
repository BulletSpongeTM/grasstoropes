using grasstoropes.ModConfiguration;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Reflection;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace grasstoropes {
    public class grasstoropesModSystem : ModSystem {
        public static ModConfig ModConfig = null!;
        public override void StartPre(ICoreAPI papi) {
            base.StartPre(papi);
            ModConfig = papi.LoadModConfig<ModConfig>("grasstoropes.json");
            if (ModConfig == null) {
                ModConfig = new ModConfig();
                papi.StoreModConfig(ModConfig, "grasstoropes.json");
            }
            handleFirePitStarter(papi);
        }

        public override void Start(ICoreAPI api) {
            api.Logger.Event("started 'Grass to Ropes' mod");
        }

        public override void StartClientSide(ICoreClientAPI capi) {
            base.StartClientSide(capi);
            //handleFlax(capi);
        }

        public override void StartServerSide(ICoreServerAPI sapi) {
            base.StartServerSide(sapi);
            handleFlax(sapi);
        }


        private void handleFlax(ICoreAPI api) {
            var recipeCode = new AssetLocation("grasstoropes:recipes/grid/flaxfibers.json");
            if (!ModConfig.flaxEnabled) {
                var recipe = api.World.GridRecipes.FirstOrDefault(r => r.Name.Equals(recipeCode));
                if (recipe != null) {
                    recipe.Enabled = false;
                }
            }
        }
        private void handleFirePitStarter(ICoreAPI api)
        {
            var modRoot = Assembly.GetExecutingAssembly().Location;
            var modRootDir = Path.GetDirectoryName(modRoot)!;
            modRoot = Path.GetFullPath(Path.Combine(modRootDir, "."));
            var bundleDir = Path.Combine(modRoot, "assets", "grasstoropes", "itemtypes", "resource", "grassbundle");
            var fullpath = Path.Combine(bundleDir, "grassbundle.json");
            if (File.Exists(fullpath)) {
                var j = JObject.Parse(File.ReadAllText(fullpath));
                if (!ModConfig.usedAsFirePitStarter) {
                    //api.Logger.Event("[Grass to Ropes] usedAsFirePitStarter is false, disabling Dry grass bundle being used as a firepit starter.");
                    if (j.ContainsKey("class")) {
                        j.Remove("class");
                        File.WriteAllText(fullpath, j.ToString());
                        //api.Logger.Event("[Grass to Ropes] Dry grass bundle being used as a FirePit starter disabled.");
                    }
                } else {
                    //api.Logger.Event("[Grass to Ropes] usedAsFirePitStarter is true, enabling Dry grass bundle being used as a firepit starter.");
                    if (j.ContainsKey("class")) {
                        j["class"] = "ItemDryGrass";
                        File.WriteAllText(fullpath, j.ToString());
                        //api.Logger.Event($"[Grass to Ropes] {bundleDir} Contains key \"class\". Dry grass bundle being used as a FirePit starter enabled.");
                    } else {
                        j.Add("class", "ItemDryGrass");
                        File.WriteAllText(fullpath, j.ToString());
                        //api.Logger.Event($"[Grass to Ropes] {bundleDir} Did not contain key \"class\". Added it, Dry grass bundle being used as a FirePit starter enabled.");
                        //api.Logger.Event("[Grass to Ropes] Dry grass bundle being used as a FirePit starter enabled.");
                    }
                }
            }
            else {
                if (api is ICoreServerAPI sapi) {
                    sapi.Logger.Error($"[Grass to Ropes] Could not find \"grassbundle.json\" in {bundleDir}");
                }
            }
        }
    }
}
