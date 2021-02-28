using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
