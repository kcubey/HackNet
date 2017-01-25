using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace HackNet.Game
{
    public class Mission
    {
        // Randome Generation
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

        // Scanning for general stuff
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

        //Check mission type
        public static bool checkMissionType(string atkType)
        {
            if (atkType.Equals("PWDATK"))
                return true;
            if (atkType.Equals("SQLIN"))
                return true;
            if (atkType.Equals("MITM"))
                return true;
            if (atkType.Equals("XXS"))
                return true;

            return false;
        }

        // Gameplay for Password Attack

        public static List<string> LoadNautilus()
        {
            List<string> mList = new List<string>();
            mList.Add("bin");
            mList.Add("root");
            mList.Add("lib");
            mList.Add("tmp");
            mList.Add("secret");
            mList.Add("audit.log");
            return mList;
        }
        
        public static bool CheckStolenFile(string fileName)
        {
            if (fileName.Equals("secret"))
            {
                return true;
            }else
            {
                return false;
            }
        }

        public static List<string> LoadSuccessPwd(MissionData mis, string command = null)
        {
            List<string> succList = new List<string>();
            succList.Add("======================================");
            succList.Add("Successful Access to server: " + mis.MissionIP);
            succList.Add("Last Login: " + DateTime.Now);
            succList.Add("@HacknetHost: " + command);
            return succList;
        }

        public static List<string> LoadPwdList()
        {
            List<string> pwdList = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                string password = Membership.GeneratePassword(50, 0);
                password= Regex.Replace(password, @"[^a-zA-Z0-9]", m => "");
                pwdList.Add(password.Substring(0, 10));
            }
            return pwdList;
        }


        // Gameplay for SQLInjection
        public static List<string> LoadURLList()
        {
            List<string> urlList = new List<string>();
            urlList.Add("www.hostlogin.com/signin");
            urlList.Add("www.sqllogin.com/signin");
            urlList.Add("www.masterbox.com/signin");
            urlList.Add("www.remotehost.com/signin");
            urlList.Add("www.localhost.com/signin");

            return urlList;
        }
        public static List<string> LoadSuccessURL(MissionData mis, string command = null)
        {
            List<string> succList = new List<string>();
            succList.Add("======================================");
            succList.Add("Successful connection to the host ");
            succList.Add("Running .....");

            return succList;
        }

        public static List<string> LoadSQLCode()
        {
            List<string> codeList = new List<string>();
            codeList.Add("'OR 1=1--*");
            codeList.Add("adminbypass-'*/--");
            codeList.Add("' DROP ALL TABLES;--");

            return codeList;
        }
    }
}