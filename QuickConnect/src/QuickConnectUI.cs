using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QuickConnect
{
    class QuickConnectUI : MonoBehaviour
    {
        public static QuickConnectUI instance;

        private Rect windowRect, lastRect;
        private Rect dragRect = new Rect(0, 0, 10000, 20);

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
            windowRect = GUILayout.Window(1586463, windowRect, WindowFunction, "Quick Connect");
            if (!lastRect.Equals(windowRect))
            {
                Mod.windowPosX.Value = windowRect.x;
                Mod.windowPosY.Value = windowRect.y;
                lastRect = windowRect;
            }
        }

        void WindowFunction(int windowID)
        {
            GUI.DragWindow(dragRect);
            if (Servers.entries.Count > 0)
            {
                foreach (var ent in Servers.entries)
                {
                    if (GUILayout.Button(ent.name))
                    {
                        ent.DoConnect();
                    }
                }
            } else
            {
                GUILayout.Label("No servers defined");
                GUILayout.Label("Add quick_connect_servers.cfg");
            }
        }

    }
}
