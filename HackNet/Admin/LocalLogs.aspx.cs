using HackNet.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
	public partial class LocalLogs : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			LogsLabel.Text = Logger.GetLogFile().Replace(Environment.NewLine, "<br/>");
		}
	}
}