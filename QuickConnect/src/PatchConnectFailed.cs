using HarmonyLib;

namespace QuickConnect
{
    [HarmonyPatch(typeof(ZSteamMatchmaking), "OnJoinServerFailed")]
    class PatchConnectFailed
    {
        static void Postfix()
        {
            if (QuickConnectUI.instance)
                QuickConnectUI.instance.JoinServerFailed();
        }
    }
}
