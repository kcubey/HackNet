using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet
{
	public partial class Error : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string status = Request.QueryString["Code"];

			if (status == null)
			{
				ErrorInfo.Visible = false;
				return;
			}

			string description = "";

			switch (status)
			{
				case "401":
					description = "You are not authorized to access this page";
					break;
				case "404":
					description = "This page was nowhere to be found!";
					break;
			}

			ErrorDescription.Text = description;
		}
	}
}