using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace QuickConnect
{
    [BepInPlugin("net.bdew.valheim.QuickConnect", "QuickConnect", "1.0.0")]
    class Mod : BaseUnityPlugin
    {
        public static ManualLogSource Log;

        void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("QuickConnect");
            var harmony = new Harmony("net.bdew.valheim.QuickConnect");
            harmony.PatchAll();
        }
    }
}
