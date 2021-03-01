using HarmonyLib;
using UnityEngine;

namespace QuickConnect
{
    [HarmonyPatch(typeof(FejdStartup), "OnSelelectCharacterBack")]
    class PatchCharacterBack
    {
        static void Postfix()
        {
            if (QuickConnectUI.instance)
            {
                QuickConnectUI.instance.AbortConnect();
            }
        }
    }
}
