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

        private Rect windowRect = new Rect(20, 20, 250, 50);

        void OnGUI()
        {
            GUILayout.Window(1586463, windowRect, WindowFunction, "Quick Connect");
        }

        void WindowFunction(int windowID)
        {
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
