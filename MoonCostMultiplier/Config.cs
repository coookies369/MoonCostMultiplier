using BepInEx.Configuration;
using CSync.Lib;
using CSync.Util;
using System.Runtime.Serialization;

[DataContract]
public class Config : SyncedConfig2<Config>
{
    [SyncedEntryField] public SyncedEntry<float> Multiplier;

    public Config(ConfigFile cfg) : base(MoonCostMultiplier.MyPluginInfo.PLUGIN_GUID)
    {
        Multiplier = cfg.BindSyncedEntry(
                new ConfigDefinition("General", "Multiplier"),
                0.5f,
                new ConfigDescription("Multiplier for all moon routing costs"));

        ConfigManager.Register(this);
    }
}