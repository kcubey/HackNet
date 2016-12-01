using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

using HackNet.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Http;

namespace HackNet {
    public class Global : HttpApplication {
        void Application_Start(object sender, EventArgs e) {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			if (SqlConnAvailable)
				Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Data.Configuration>("DefaultConnection"));

		}

		private static bool SqlConnAvailable
		{
			get
			{
				string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
				using (SqlConnection connection = new SqlConnection(connStr))
					try
					{
						connection.Open();
						System.Diagnostics.Debug.WriteLine("Connection to database = SUCCEEDED");
						return true;
					} catch (SqlException)
					{
						System.Diagnostics.Debug.WriteLine("Connection to database = FAILED");
						return false;
					}
			}
		}
        // Braintree
/*
        public class MvcApplication : System.Web.HttpApplication
        {
            protected void Application_Start()
            {
                AreaRegistration.RegisterAllAreas();

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
            }
        }
*/
    }
}
