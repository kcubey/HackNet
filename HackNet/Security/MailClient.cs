using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace HackNet.Security
{
	public class MailClient : IDisposable
	{
		internal string SmtpClientHost { get; set; }
		internal int SmtpClientPort { get; set; }
		internal string SmtpClientLogin { get; set; }
		internal string SmtpClientPasswd { private get; set; }
		internal string MailFrom { get; set; }
		internal string MailTo { get; set; }

		internal string Body { get; set; }
		internal string Subject { get; set; }

		internal MailClient(string receiver)
		{
			SmtpClientHost = ConfigurationManager.AppSettings["MailServer"];
			SmtpClientPort = 587;
			SmtpClientLogin = ConfigurationManager.AppSettings["MailServerLogin"];
			SmtpClientPasswd = ConfigurationManager.AppSettings["MailServerPassword"];
			MailFrom = ConfigurationManager.AppSettings["MailServerSender"];
			MailTo = receiver;
		}

		internal void AddLine(string lineToAdd)
		{
			Body += (lineToAdd + @"<br />");
		}

		internal bool Send(string receivername, string callout = "Visit Us", string calloutlink = "https://haxnet.azurewebsites.net/")
		{
			if (string.IsNullOrEmpty(Body))
				throw new MailException("Message body is empty, refusing to send.");

			if (string.IsNullOrEmpty(Subject))
				throw new MailException("Message subject is empty, refusing to send.");

			StringBuilder sb = new StringBuilder(System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/basic.html")));
			sb.Replace("{PREHEADER}", "HackNet Mail - ");
			sb.Replace("{RECEIVERNAME}", receivername);
			sb.Replace("{BODY}", Body);
			sb.Replace("{CTA}", callout);
			sb.Replace("{CTAHREF}", calloutlink);
			string message = sb.ToString();
			System.Diagnostics.Debug.WriteLine(message);

			MailMessage mail = new MailMessage(MailFrom, MailTo, Subject, message);
			AlternateView altview = AlternateView.CreateAlternateViewFromString(message, new ContentType("text/html"));
			mail.From = new MailAddress(MailFrom, "HackNet");
			mail.AlternateViews.Add(altview);

			SmtpClient client = new SmtpClient(SmtpClientHost, SmtpClientPort);
			client.Credentials = new NetworkCredential(SmtpClientLogin, SmtpClientPasswd);
			client.EnableSsl = true;

			try
			{
				client.Send(mail);
				return true;
			} catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.StackTrace);
				return false;
			}
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~MailClient() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion
	}
}