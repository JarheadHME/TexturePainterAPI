using BepInEx;
using BepInEx.Unity.IL2CPP;
using GTFO.API;
using HarmonyLib;
using System.Linq;

namespace TexturePainterAPI;
[BepInPlugin("TexturePainterAPI", "TexturePainterAPI", VersionInfo.Version)]
[BepInDependency("dev.gtfomodding.gtfo-api", BepInDependency.DependencyFlags.HardDependency)]
internal class EntryPoint : BasePlugin
{
    public override void Load()
    {
        AssetAPI.OnAssetBundlesLoaded += AssetAPI_OnAssetBundlesLoaded;
    }

    private void AssetAPI_OnAssetBundlesLoaded()
    {
        Assets.Init();
    }
}
