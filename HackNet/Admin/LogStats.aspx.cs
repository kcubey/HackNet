using HackNet.Data;
using HackNet.Loggers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
	public partial class LogStats : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}



		protected string jsArrayS7D()
		{
			var tbl = Severity7D();
			return (new JavaScriptSerializer()).Serialize(tbl);
		}

		protected string jsArrayS24H()
		{
			var tbl = Severity24H();
			return (new JavaScriptSerializer()).Serialize(tbl);
		}

		protected string jsArrayT7D()
		{
			var tbl = Type7D();
			return (new JavaScriptSerializer()).Serialize(tbl);
		}

		private DateTime RoundUp(DateTime dt, TimeSpan d)
		{
			return new DateTime(((dt.Ticks + d.Ticks - 1) / d.Ticks) * d.Ticks);
		}


		protected List<object> Severity7D()
		{
			using (DataContext db = new DataContext())
			{
				var final = new List<object>();

				for (int i = 6; i >= 0; i--)
				{
					DateTime daystart = RoundUp(DateTime.Now.AddDays(-(i) - 1), TimeSpan.FromDays(1));
					DateTime dayend = daystart.AddDays(1);
					int info = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Severity == (int)LogSeverity.INFO)
						.Count();


					int warn = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Severity == (int)LogSeverity.WARN)
						.Count();


					int err = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Severity == (int)LogSeverity.ERROR)
						.Count();

					final.Add(new object[] {
						daystart.ToShortDateString(),
						info,
						warn,
						err
					});
				}

				return final;
			}
		}

		protected List<object> Severity24H()
		{
			using (DataContext db = new DataContext())
			{
				var final = new List<object>();
				for (int i = 23; i >= 0; i--)
				{
					DateTime start = RoundUp(DateTime.Now.AddHours(-(i) - 1), TimeSpan.FromHours(1));
					DateTime end = start.AddDays(1);
					int info = db.Logs.Where(
						le => le.Timestamp > start
						&& le.Timestamp < end
						&& le.Severity == (int)LogSeverity.INFO)
						.Count();

					int warn = db.Logs.Where(
						le => le.Timestamp > start
						&& le.Timestamp < end
						&& le.Severity == (int)LogSeverity.WARN)
						.Count();


					int err = db.Logs.Where(
						le => le.Timestamp > start
						&& le.Timestamp < end
						&& le.Severity == (int)LogSeverity.ERROR)
						.Count();

					final.Add(new object[] {
						start.ToShortTimeString(),
						info,
						warn,
						err
					});
				}

				return final;
			}
		}

		protected List<object> Type7D()
		{
			using (DataContext db = new DataContext())
			{
				var final = new List<object>();


				for (int i = 6; i >= 0; i--)
				{
					DateTime daystart = RoundUp(DateTime.Now.AddDays(-(i) - 1), TimeSpan.FromDays(1));
					DateTime dayend = daystart.AddDays(1);
					int game = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Type == (int)LogType.Game)
						.Count();

					int security = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Type == (int)LogType.Security)
						.Count();

					int payment = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Type == (int)LogType.Payment)
						.Count();

					int profile = db.Logs.Where(
						le => le.Timestamp > daystart
						&& le.Timestamp < dayend
						&& le.Type == (int)LogType.Profile)
						.Count();

					final.Add(new object[] {
						daystart.ToShortDateString(),
						game,
						security,
						profile,
						payment
					});
				}


				return final;
			}
		}
	}
}