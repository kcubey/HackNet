using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Game
{
    public class Mission
    {
        internal string IPaddress { get; set; }
        internal string Objective { get; set; }
        internal int numOfPorts { get; set; }
        internal string system { get; set; }
        public Mission()
        {

        }

        public static string GetRandomIp()
        {
            Random _random = new Random();
            return string.Format("{0}.{1}.{2}.{3}", _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
        }
        public static string GetRandomMacAddress()
        {
            var random = new Random();
            var buffer = new byte[6];
            random.NextBytes(buffer);
            var result = String.Concat(buffer.Select(x => string.Format("{0}:", x.ToString("X2"))).ToArray());
            return result.TrimEnd(':');
        }

        private static string GetrandomSystem()
        {
            List<string> sysList = new List<string>();
            sysList.Add("CentOS Linux 7");
            sysList.Add("Ubuntu 16.04");
            sysList.Add("Linux 2.6.10");
            sysList.Add("Oracle Linux 7");
            Random rnd = new Random();
            int r = rnd.Next(sysList.Count);
            string system = sysList[r];
            return system;
        }

        public static List<string> scanMission(MissionData mission, string username,bool cond)
        {
            Random rnd = new Random();
            string console = username + "@HackNet: ~#  ";
            int current = rnd.Next(10, 1000);

            List<string> scanList = new List<string>();

            scanList.Add(console + "Hmap " + mission.MissionIP);
            scanList.Add("Starting Hmap 8.88 at " + DateTime.Now);
            scanList.Add("Interesting Ports on " + mission.MissionIP);
            scanList.Add("Number of ports exposed: " + current);
            scanList.Add("Ports  " + "&nbsp;&nbsp;" + "  STATE  " + "&nbsp;&nbsp;" + "  SERVICE");
            scanList.Add("22/tcp  " + "&nbsp;&nbsp;" + "  open  " + "&nbsp;&nbsp;" + "  ssh");
            scanList.Add("25/tcp  " + "&nbsp;&nbsp;" + "  open  " + "&nbsp;&nbsp;" + "  smtp");
            scanList.Add("53/tcp  " + "&nbsp;&nbsp;" + "  open  " + "&nbsp;&nbsp;" + "  domain");
            scanList.Add("==============================================");
            scanList.Add("Server Info: ");
            scanList.Add("MAC Address: "+ GetRandomMacAddress());
            scanList.Add("Device Type: general purpose");
            scanList.Add("System: "+ GetrandomSystem());
            scanList.Add("==============================================");
            if (cond)
            {
                scanList.Add("Choose Mode of Attack: ");
                scanList.Add("1. Password Attack (PWDATK) ");
                scanList.Add("2. SQL Injection (SQLIN) ");
                scanList.Add("3. Cross Site Scripting (XSS) ");
                scanList.Add("4. Man in the Middle Attack (MITM) ");
                scanList.Add("&nbsp;&nbsp; To choose attack type in the acronym in the (_) ");
                scanList.Add("&nbsp;&nbsp; For Example; ");
                scanList.Add("&nbsp;&nbsp; To choose password attack, key in PWDATK");
                scanList.Add("==============================================");
            }
            return scanList;
        }

    }
}