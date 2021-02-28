using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickConnect
{
    class Servers
    {
        public static string ConfigPath = Path.GetDirectoryName(Paths.BepInExConfigPath) + Path.DirectorySeparatorChar + "quick_connect_servers.cfg";

        public static string currentPass;

        public class Entry
        {
            public string name;
            public string ip;
            public int port;
            public string pass;

            public override string ToString()
            {
                return string.Format("Server(name={0},ip={1},port={2}}", name, ip, port);
            }

            public void DoConnect()
            {
                currentPass = pass;
                ZSteamMatchmaking.instance.QueueServerJoin(string.Format("{0}:{1}", ip, port));
            }
        }

        public static List<Entry> entries = new List<Entry>();

        public static void Init()
        {
            entries.Clear();
            try
            {
                if (File.Exists(ConfigPath))
                {
                    using (var file = new StreamReader(ConfigPath))
                    {
                        string line;
                        while ((line = file.ReadLine()) != null)
                        {
                            line = line.Trim();
                            if (line.Length == 0 || line.StartsWith("#")) continue;
                            var split = line.Split(':');
                            if (split.Length == 4)
                            {
                                var aName = split[0];
                                var aIp = split[1];
                                var aPort = int.Parse(split[2]);
                                var aPass = split[3];
                                entries.Add(new Entry
                                {
                                    name = aName,
                                    ip = aIp,
                                    port = aPort,
                                    pass = aPass,
                                });
                            }
                            else
                            {
                                Mod.Log.LogWarning("Invalid config line: " + line);
                            }
                        }
                        Mod.Log.LogInfo(string.Format("Loaded {0} server entries", entries.Count));
                    }
                }
            }
            catch (Exception ex)
            {
                Mod.Log.LogError(string.Format("Error loading config {0}", ex));
            }
        }
    }
}
