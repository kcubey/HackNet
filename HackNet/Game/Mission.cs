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
        internal int numOfFirewall { get; set; }
        internal int numOfPorts { get; set; }

        public Mission()
        {

        }

        public List<string> scanMission(Mission mission,string username)
        {
            string console=username+ "@HackNet: ~#  ";

            List <string> scanList = new List<string>();
            scanList.Add(console + "Hmap "+mission.IPaddress);
            scanList.Add("Starting Hmap 8.88 at " + DateTime.Now);
            scanList.Add("Host is up");
            scanList.Add("Server Info: ");



            return scanList;
        }
    }
}