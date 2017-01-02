using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HackNet.Game.Class
{
    public class MissionPwdAtk
    {
        internal MissionData mis { get; set; }
        internal string target { get; set; }
        internal string atkMethod { get; set; }

        public static List<string> LoadSuccessPwd(MissionData mis)
        {
            List<string> succList = new List<string>();
            succList.Add("======================================");
            succList.Add("Successful Access to server: "+mis.MissionIP);
            succList.Add("Last Login: "+DateTime.Now);
            return succList;
        }

        public static List<string> LoadPwdList()
        {
            List<string> pwdList = new List<string>();
            for(int i = 0; i < 10; i++)
            {
                string password = Membership.GeneratePassword(10, 4);
                pwdList.Add(password);
            }
            
            return pwdList;
        }
    }
}