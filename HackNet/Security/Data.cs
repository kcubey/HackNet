using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;

namespace HackNet.Security {
	public class Data : IDisposable
	{

		private string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
		private static Stopwatch sw;
		private static int Queries = 0;
		private SqlConnection conn;

		private SqlConnection Connection 
		{
			get {
				if (conn != null && conn.State == ConnectionState.Open)
				{
					conn.Close();
				}

				conn = new SqlConnection(connstring);
				conn.Open();
				Queries++;
				return conn;
			}
		}

		internal Data([CallerMemberName] string memberName = "")
		{
			// Print out caller class
			sw = System.Diagnostics.Stopwatch.StartNew();
			System.Diagnostics.Debug.Write("\n" + Queries + ". DataAccess Entity Created From: " + memberName);
		}




		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					sw.Stop();
					System.Diagnostics.Debug.Write("...done! (Took " + sw.ElapsedMilliseconds + "ms)");
					if (conn != null)
					{
						conn.Close();
						conn.Dispose();
					}
				}
			}
			disposedValue = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

	}
}