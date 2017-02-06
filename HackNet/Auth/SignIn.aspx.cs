using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using HackNet.Security;
using HackNet.Data;
using HackNet.Loggers;

using System.Web.Security;
using System.Threading.Tasks;
using System.Threading;

namespace HackNet.Auth {
	public partial class SignIn : Page {

		protected void Page_Load(object sender, EventArgs e)
		{
			if (CurrentUser.IsAuthenticated())
				Response.Redirect("~/Game/Home");

            int delay = RateLimit.Instance.GetDelay(Authenticate.GetIP());

            if (delay > 0)
            {
                DelayMsg.Text = @" You have had failed logins, your attempt has been delayed: " + delay + "secs" ;
            }
        }

        protected void LoginClick(object sender, EventArgs e)
        {
            // Gets the number of seconds that this user has been delayed for
            int delaysecs = RateLimit.Instance.GetDelay(Authenticate.GetIP());
            bool delayedloginsuccess = false;

			// Force email lowercase before validating
			string email = Email.Text.ToLower();
			string ip = Authenticate.GetIP();
			try
			{
				using (Authenticate auth = new Authenticate(email))
				{

					// ValidateLogin generates a KeyStore and stores it as a temp variable
					AuthResult result = auth.ValidateLogin(UserPass.Text);

					switch (result)
					{
						case AuthResult.Success:
                            Msg.Text = "Login Successful";
                            delayedloginsuccess = true;
							break;
						case AuthResult.PasswordIncorrect:
							Msg.Text = "User and/or password not found (1)";
							RateLimit.Instance.AddOrUpdateAttempt(ip);
							break;
						case AuthResult.UserNotFound:
							Msg.Text = "User and/or password not found (2)";
							RateLimit.Instance.AddOrUpdateAttempt(ip);
							break;
						case AuthResult.EmailNotVerified:
							Msg.Text = "Email has not been verified, a confirmation email will be sent to you shortly";
							RateLimit.Instance.AddOrUpdateAttempt(ip);
							break;
						default:
							Msg.Text = "Unhandled error has occured";
							break;
					}

                    // Forces a time delay using delayer task specified delay duration
                    Thread.Sleep(TimeSpan.FromSeconds(delaysecs));

                    if (delayedloginsuccess == true)
                        LoginSuccess(email, auth.TempKeyStore);
                }
			} catch (UserException)
			{
				Msg.Text = "User and/or password not found (Error)";
			}
        }

        /// <summary>
        /// Executed on successful login
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ks"></param>
        private void LoginSuccess(string email, KeyStore ks)
		{
			using (Authenticate a = new Authenticate(email))
			{
				string redir = Request.QueryString["ReturnUrl"];

				if (!IsLocalUrl(redir))
					redir = "/";

				if (string.IsNullOrWhiteSpace(redir))
					redir = "~/Default";
				else if (redir.Equals("/"))
					redir = "~/Default";
				else
					redir = Request.QueryString["ReturnUrl"];
					
				if (a.Is2FAEnabled)
				{
					Session["TempKeyStore"] = ks;
					Session["Cookie"] = a.AuthCookie;
					Session["PasswordSuccess"] = email;
					Session["ReturnUrl"] = redir;
					Session["UserId"] = a.UserId;
					Response.Redirect("~/Auth/OtpVerify");
				} else
				{
					Session["KeyStore"] = ks;
					Response.Cookies.Add(a.AuthCookie);
					Response.Redirect(redir);
				}
			}
		}

		// Method to validate for Open Redirect vulnerability
		private bool IsLocalUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return false;
			}

			Uri absoluteUri;
			if (Uri.TryCreate(url, UriKind.Absolute, out absoluteUri))
			{
				return Request.Url.Host.Equals(absoluteUri.Host, StringComparison.OrdinalIgnoreCase);
			}
			else
			{
				bool isLocal = !url.StartsWith("http:", StringComparison.OrdinalIgnoreCase)
					&& !url.StartsWith("https:", StringComparison.OrdinalIgnoreCase)
					&& Uri.IsWellFormedUriString(url, UriKind.Relative);
				return isLocal;
			}
		}
	}
}