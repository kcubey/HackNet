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

		private static short isTesting = -1; // 0 for no, 1 for yes

        void Application_Start(object sender, EventArgs e) {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
			if (SqlConnAvailable)
				Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Data.Configuration>("DefaultConnection"));
		}

		internal static bool SqlConnAvailable
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

		internal static bool IsInUnitTest
		{
			get {

				if (isTesting == -1) // Get once and cache the result
				{
					bool testing = AppDomain.CurrentDomain.GetAssemblies()
						.Any(a => a.FullName.StartsWith("Microsoft.VisualStudio.QualityTools.UnitTestFramework"));
					if (testing)
						isTesting = 1;
					else
						isTesting = 0;
				}

				if (isTesting == 1)
				{
					return true;
				} else
				{
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
