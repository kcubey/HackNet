using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet
{
	public partial class AdminMaster : System.Web.UI.MasterPage
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			switch(CurrentUser.GetAccessLevel())
			{
				case AccessLevel.Admin:
				case AccessLevel.Staff:
					break;
				default:
					Response.Redirect("~/Default");
					Response.End();
					break;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}