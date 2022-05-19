using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace QuickConnect
{
    [BepInPlugin("net.bdew.valheim.QuickConnect", "QuickConnect", "1.5.0")]
    class Mod : BaseUnityPlugin
    {
        public static ManualLogSource Log;

        public static ConfigEntry<float> windowPosX;
        public static ConfigEntry<float> windowPosY;
        public static ConfigEntry<int> buttonFontSize;
        public static ConfigEntry<int> labelFontSize;
        public static ConfigEntry<int> windowWidth;
        public static ConfigEntry<int> windowHeight;

        void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("QuickConnect");
            windowPosX = Config.Bind("UI", "WindowPosX", 20f);
            windowPosY = Config.Bind("UI", "WindowPosY", 20f);
            buttonFontSize = Config.Bind("UI", "ButtonFontSize", 0);
            labelFontSize = Config.Bind("UI", "LabelFontSize", 0);
            windowWidth = Config.Bind("UI", "WindowWidth", 250);
            windowHeight = Config.Bind("UI", "WindowHeight", 50);
            var harmony = new Harmony("net.bdew.valheim.QuickConnect");
            harmony.PatchAll();
        }
    }
}
