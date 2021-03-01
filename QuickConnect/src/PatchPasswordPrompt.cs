using HarmonyLib;
using System.Reflection;

namespace QuickConnect
{
    [HarmonyPatch(typeof(ZNet), "RPC_ClientHandshake")]
    class PatchPasswordPrompt
    {
        static bool Prefix(ZNet __instance, ZRpc rpc, bool needPassword)
        {
            string currentPass = QuickConnectUI.instance.CurrentPass();
            if (currentPass != null)
            {
                if (needPassword)
                {
                    Mod.Log.LogInfo("Authenticating with saved password...");
                    MethodInfo dynMethod = typeof(ZNet).GetMethod("SendPeerInfo", BindingFlags.NonPublic | BindingFlags.Instance);
                    dynMethod.Invoke(__instance, new object[] { rpc, currentPass });
                    return false;
                }
                Mod.Log.LogInfo("Server didn't want password?");
            }
            return true;
        }
    }
}
