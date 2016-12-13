using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Game.Class
{
    public class Workstations
    {
        public string WorkstnName { get; set; }
        public string Processor { get; set; }
        public string Graphicard { get; set; }
        public string Memory { get; set; }
        public string Powersupply { get; set; }
        // Workstation attributes
        public int HpAtrb { get; set; }
        public int AtkAtrb { get; set; }
        public int DefAtrb { get; set; }
        public double SpeedAtrb { get; set; }

        public Workstations()
        {

        }
        public static Workstations Getworkstation(string username)
        {
            Workstations workstn = new Workstations();
            workstn.WorkstnName = username + "'s Computer";
            workstn.Processor = "Intel I7 5th Generation";
            workstn.Graphicard = "Myvidia";
            workstn.Memory = "1mb";
            workstn.Powersupply = "100watts";
            workstn.HpAtrb = 10;
            workstn.AtkAtrb = 10;
            workstn.DefAtrb = 10;
            workstn.SpeedAtrb = 10;
            return workstn;
        }

    }
}