using HackNet.Game.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Workstation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Workstations workstn = Workstations.Getworkstation("Testuser");
            WorkstationNameLbl.Text=workstn.WorkstnName;
            ProcessorLbl.Text = workstn.Processor;
            GraphicLbl.Text = workstn.Graphicard;
            MemoryLbl.Text = workstn.Memory;
            PwsupLbl.Text = workstn.Powersupply;



        }

    }
}