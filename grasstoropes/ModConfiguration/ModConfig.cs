namespace grasstoropes.ModConfiguration;

public class ModConfig
{
    public string _flaxComment { get; set; } = "Enable the crafting of Dry grass bundles, into flax fibers. true/false. Default: false";
    public bool flaxEnabled { get; set; } = false;
    public string _firepitComment { get; set; } = "Enable the use of Dry grass bundles in the creation of firepits, for convenience, in place of normal dry grass. true/false. Default: false";
    public bool usedAsFirePitStarter { get; set; } = false;
}