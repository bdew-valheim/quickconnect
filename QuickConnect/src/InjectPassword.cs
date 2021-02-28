using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QuickConnect
{
    [HarmonyPatch(typeof(ZNet), "RPC_ClientHandshake")]
    class InjectPassword
    {
        static bool Prefix(ZNet __instance, ZRpc rpc, bool needPassword)
        {
            if (Servers.currentPass != null)
            {
                if (needPassword)
                {
                    Mod.Log.LogInfo("Authenticating with saved password...");
                    MethodInfo dynMethod = typeof(ZNet).GetMethod("SendPeerInfo", BindingFlags.NonPublic | BindingFlags.Instance);
                    dynMethod.Invoke(__instance, new object[] { rpc, Servers.currentPass });
                    Servers.currentPass = null;
                    return false;
                }
                Mod.Log.LogInfo("Server didn't want password?");
            }
            return true;
        }
    }
}
