using HackNet.Loggers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
	public partial class Logs : System.Web.UI.Page
	{
		private static DateTime DEFAULT_START = new DateTime(1970, 1, 1);
		private static DateTime DEFAULT_END = DateTime.Now; 

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				SearchFilter sf = new SearchFilter()
				{
					Type = LogType.Security,
					Start = DEFAULT_START,
					End = DEFAULT_END
				};
				ResultGrid.DataSource = GetDataTable(sf);
				ResultGrid.DataBind();
			}
		}

		// On category selection change
		protected void DDCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
			ResultGrid.DataSource = GetDataTable(GetFilters());
			ResultGrid.DataBind();
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
			switch (DDCategory.SelectedValue)
			{
				case "Game":
					sf.Type = LogType.Game;
					break;
				case "Security":
					sf.Type = LogType.Security;
					break;
				case "Payment":
					sf.Type = LogType.Payment;
					break;
				case "Profile":
					sf.Type = LogType.Profile;
					break;
				case "Error":
					sf.Type = LogType.Error;
					break;
				default:
					throw new LogTypeInvalidException();
			}

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

			return sf;
		}

		protected DataTable GetDataTable(SearchFilter sf)
		{
			Logger retriever = Logger.GetRetriever(sf);
			List<LogEntry> entries = retriever.Retrieve(sf);
			entries.Reverse();

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

				dt.Rows.Add(row);
			}
			return dt;
		}
	}
}