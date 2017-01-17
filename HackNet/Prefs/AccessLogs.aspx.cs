using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Loggers;
using System.Data;

namespace HackNet.Prefs
{
	public partial class AccessLogs : System.Web.UI.Page
	{
		private static readonly DateTime DEFAULT_START = new DateTime(1970, 1, 1);
		private static readonly DateTime DEFAULT_END = DateTime.Now; 

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SearchFilter sf = new SearchFilter()
				{
					Type = LogType.Security,
					Start = DEFAULT_START,
					End = DEFAULT_END,
					UserId = Authenticate.GetUserId()
				};
				ResultGrid.DataSource = GetDataTable(sf);
				ResultGrid.DataBind();
			}
		}

		// For pagination
		protected void ResultGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			ResultGrid.PageIndex = e.NewPageIndex;
			ResultGrid.DataSource = GetDataTable(GetFilters());
			ResultGrid.DataBind();
		}

		protected void SubmitBtn_Click(object sender, EventArgs e)
		{
			ResultGrid.DataSource = GetDataTable(GetFilters());
			ResultGrid.DataBind();
		}

		protected SearchFilter GetFilters()
		{
			SearchFilter sf = new SearchFilter();

			DateTime start, end = DateTime.Now;
			bool parseOK = true;

			// Checking for input for start
			if (string.IsNullOrWhiteSpace(StartDT.Text))
				start = DEFAULT_START;
			else
				parseOK = DateTime.TryParse(StartDT.Text, out start);

			// Checking for input for end
			if (string.IsNullOrWhiteSpace(EndDT.Text))
				end = DEFAULT_END;
			else
				parseOK = (parseOK && DateTime.TryParse(EndDT.Text, out end)); // Binary OR operator
			// Return result
			if (!parseOK)
				Msg.Text = "Either start or end date is invalid.";

			sf.Start = start;
			sf.End = end;
			sf.Type = LogType.Security;
			sf.UserId = Authenticate.GetUserId();

			return sf;
		}

		protected DataTable GetDataTable(SearchFilter sf)
		{
			Logger retriever = Logger.GetRetriever(sf);
			List<LogEntry> entries = retriever.Retrieve(sf);
			entries.Reverse();

			DataTable dt = new DataTable();
			dt.Columns.Add("Timestamp");
			dt.Columns.Add("Description");
			dt.Columns.Add("Severity");
			dt.Columns.Add("UA & IP");
			foreach (LogEntry e in entries)
			{
				DataRow row = dt.NewRow();
				row["Timestamp"] = e.Timestamp;
				row["Description"] = e.Description;
				row["Severity"] = e.Severity;
				row["UA & IP"] = e.IPAddress;

				dt.Rows.Add(row);
			}
			return dt;
		}
	}
}