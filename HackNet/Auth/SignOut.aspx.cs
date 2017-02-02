using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Auth
{
	public partial class SignOut : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			// Call the FormsAuthentication SignOut method to remove the cookie
			FormsAuthentication.SignOut();

			// Removes all information regarding this session
			Session.RemoveAll();

			string returnurl = Request.QueryString["ReturnUrl"];
			
			// Redirect back to sign in page
			if (returnurl == null)
				Response.Redirect("~/Auth/SignIn");
			else
				Response.Redirect("~/Auth/SignIn?ReturnUrl=" + returnurl);


		}
	}
}