using HackNet.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
	public partial class Logs : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ResultGrid.DataSource = GetDataTable(LogType.Security);
			ResultGrid.DataBind();
		}

		protected DataTable GetDataTable(LogType type)
		{
			List<LogEntry> entries = AuthLogger.Instance.Retrieve(0, null, null);
			DataTable dt = new DataTable();
			dt.Columns.Add("Timestamp");
			dt.Columns.Add("User ID");
			dt.Columns.Add("Description");
			dt.Columns.Add("Severity");
			dt.Columns.Add("UA & IP");
			foreach (LogEntry e in entries)
			{
				DataRow row = dt.NewRow();
				row["Timestamp"] = e.Timestamp;
				row["User ID"] = e.UserId;
				row["Description"] = e.Description;
				row["Severity"] = e.Severity;
				row["UA & IP"] = e.IPAddress;
			}
			return dt;
		}
	}
}