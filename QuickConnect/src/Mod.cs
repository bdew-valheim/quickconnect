using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace QuickConnect
{
    [BepInPlugin("net.bdew.valheim.QuickConnect", "QuickConnect", "1.4.0")]
    class Mod : BaseUnityPlugin
    {
        public static ManualLogSource Log;

        public static ConfigEntry<float> windowPosX;
        public static ConfigEntry<float> windowPosY;

        void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("QuickConnect");
            windowPosX = Config.Bind("UI", "WindowPosX", 20f);
            windowPosY = Config.Bind("UI", "WindowPosY", 20f);
            var harmony = new Harmony("net.bdew.valheim.QuickConnect");
            harmony.PatchAll();
        }
    }
}
