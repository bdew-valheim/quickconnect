using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;

namespace QuickConnect
{
    class QuickConnectUI : MonoBehaviour
    {
        public static QuickConnectUI instance;

        private Rect windowRect, lastRect, errorWinRect;
        private Rect dragRect = new Rect(0, 0, 10000, 20);


        private Task<IPHostEntry> resolveTask;
        private Servers.Entry connecting;
        private string errorMsg;

        void Update()
        {
            if (resolveTask != null)
            {
                if (resolveTask.IsFaulted)
                {
                    Mod.Log.LogError($"Error resolving IP: {resolveTask.Exception}");
                    if (resolveTask.Exception is AggregateException)
                    {
                        ShowError((resolveTask.Exception as AggregateException).InnerException.Message);
                    }
                    else
                    {
                        ShowError(resolveTask.Exception.Message);
                    }
                    resolveTask = null;
                    connecting = null;
                }
                else if (resolveTask.IsCanceled)
                {
                    resolveTask = null;
                    connecting = null;
                }
                else if (resolveTask.IsCompleted)
                {
                    foreach (var addr in resolveTask.Result.AddressList)
                    {
                        if (addr.AddressFamily == AddressFamily.InterNetwork)
                        {
                            Mod.Log.LogInfo($"Resolved: {addr}");
                            resolveTask = null;
                            ZSteamMatchmaking.instance.QueueServerJoin($"{addr}:{connecting.port}");
                            return;
                        }
                    }
                    resolveTask = null;
                    connecting = null;
                    ShowError("Server DNS resolved to no valid addresses");
                }
            }
        }

        void Awake()
        {
            windowRect.x = Mod.windowPosX.Value;
            windowRect.y = Mod.windowPosY.Value;
            windowRect.width = 250;
            windowRect.height = 50;
            lastRect = windowRect;
        }

        void OnGUI()
        {
            if (errorMsg != null)
            {
                errorWinRect = GUILayout.Window(1586464, errorWinRect, DrawErrorWindow, "Error");
            }
            else
            {
                windowRect = GUILayout.Window(1586463, windowRect, DrawConnectWindow, "Quick Connect");
                if (!lastRect.Equals(windowRect))
                {
                    Mod.windowPosX.Value = windowRect.x;
                    Mod.windowPosY.Value = windowRect.y;
                    lastRect = windowRect;
                }
            }
        }

        void DrawConnectWindow(int windowID)
        {
            GUI.DragWindow(dragRect);
            if (connecting != null)
            {
                GUILayout.Label("Connecting to " + connecting.name);
                if (GUILayout.Button("Cancel"))
                {
                    AbortConnect();
                }
            }
            else if (Servers.entries.Count > 0)
            {
                foreach (var ent in Servers.entries)
                {
                    if (GUILayout.Button(ent.name))
                    {
                        DoConnect(ent);
                    }
                }
            }
            else
            {
                GUILayout.Label("No servers defined");
                GUILayout.Label("Add quick_connect_servers.cfg");
            }
        }

        void DrawErrorWindow(int windowID)
        {
            GUILayout.Label(errorMsg);
            if (GUILayout.Button("Close"))
            {
                errorMsg = null;
            }
        }

        private void DoConnect(Servers.Entry server)
        {
            connecting = server;
            try
            {
                IPAddress.Parse(server.ip);
                ZSteamMatchmaking.instance.QueueServerJoin($"{server.ip}:{server.port}");
            }
            catch (FormatException)
            {
                Mod.Log.LogInfo($"Resolving: {server.ip}");
                resolveTask = Dns.GetHostEntryAsync(server.ip);
            }
        }

        public string CurrentPass()
        {
            if (connecting != null)
                return connecting.pass;
            return null;
        }

        public void JoinServerFailed()
        {
            ShowError("Server connection failed");
            connecting = null;
        }

        public void ShowError(string msg)
        {
            errorMsg = msg;
            errorWinRect = new Rect(Screen.width / 2 - 125, Screen.height / 2, 250, 30);
        }

        public void AbortConnect()
        {
            connecting = null;
            resolveTask = null;
        }
    }
}
