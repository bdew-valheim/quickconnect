﻿using HarmonyLib;
using UnityEngine;

namespace QuickConnect
{
    [HarmonyPatch(typeof(FejdStartup), "SetupGui")]
    class InjectUI
    {
        static void Postfix()
        {
            if (!QuickConnectUI.instance)
            {
                Servers.Init();
                var go = new GameObject("QuickConnect");
                QuickConnectUI.instance = go.AddComponent<QuickConnectUI>();
            }
        }
    }
}
