using Microsoft.AspNet.FriendlyUrls;
using System;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Microsoft.AspNet.FriendlyUrls.Resolvers;

namespace HackNet
{
	public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings, new SiteMobileMasterDisable());
			routes.MapPageRoute("DefRedirect", "Default", "~/Game/Home.aspx");
			routes.MapPageRoute("DefAspxRedirect", "Default.aspx", "~/Game/Home.aspx");
		}

    }

	public class SiteMobileMasterDisable : WebFormsFriendlyUrlResolver
	{
		protected override bool TrySetMobileMasterPage(HttpContextBase httpContext, Page page, string mobileSuffix)
		{
			if (mobileSuffix == "Mobile")
				return false;
			else
				return base.TrySetMobileMasterPage(httpContext, page, mobileSuffix);
		}
	}
}
