using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HackNet.Security
{
	public class CurrentUser
	{

		/// <summary>
		/// Checks if the current user is authenticated
		/// </summary>
		/// <param name="email">Checks if the authenticated user has this email</param>
		/// <returns>Whether the current user is authenticated</returns>
		internal static bool IsAuthenticated(string email = null)
		{
			if (Global.IsInUnitTest)
				return true;

			if (HttpContext.Current == null)
				throw new AuthException("Current HTTP Context is NULL");

			if (email == null)
			{
				return HttpContext.Current.User.Identity.IsAuthenticated;
			}
			else
			{
				return (HttpContext.Current.User.Identity.Name.Equals(email.ToLower()));
			}
		}

		/// <summary>
		/// Gets the email address of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static string GetEmail()
		{
			if (!IsAuthenticated())
				throw new AuthException("User is not logged in");

			return HttpContext.Current.User.Identity.Name;
		}

		/// <summary>
		/// Gets the User ID of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static int GetUserId()
		{

			string[] UserData = GetUserData();
			int UserId;
			if (!int.TryParse(UserData[0], out UserId))
				throw new AuthException("UserId is not an integer");

			return UserId;
		}

		/// <summary>
		/// Gets the username of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static string GetUserName()
		{

			string[] UserData = GetUserData();

			return UserData[1];
		}

		/// <summary>
		/// Gets the AccessLevel enum of the current user, no DB required
		/// </summary>
		/// <returns></returns>
		internal static AccessLevel GetAccessLevel()
		{
			if (Global.IsInUnitTest)
				return AccessLevel.User;

			string[] UserData = GetUserData();
			int AccessLevelNum;
			if (!int.TryParse(UserData[2], out AccessLevelNum))
				throw new AuthException("AccessLevel is not an integer");

			if (!Enum.IsDefined(typeof(AccessLevel), (AccessLevel)AccessLevelNum))
				throw new AuthException("Invalid Access Level" + AccessLevelNum);

			return (AccessLevel)AccessLevelNum;
		}

		/// <summary>
		/// PRIVATE: Read the user data and parse into string array
		/// </summary>
		/// <returns></returns>
		private static string[] GetUserData()
		{
			if (Global.IsInUnitTest)
				return new string[] { "6", "UnitTester", "hacknet@wlgoh.com" };

			if (!CurrentUser.IsAuthenticated())
				throw new AuthException("User is not logged in");

			FormsIdentity ident = HttpContext.Current.User.Identity as FormsIdentity;
			if (ident == null)
				throw new AuthException("Forms Authentication Identity is not authenticated");

			string[] UserData = ident.Ticket.UserData.Split(';');

			if (UserData.Length != 3)
				throw new AuthException("UserData is of incorrect length");

			return UserData;
		}


		/// <summary>
		/// Get the user entity of the current logged in user
		/// </summary>
		/// <param name="ReadOnly">OPTIONAL: Whether to enable EF tracking on the user</param>
		/// <param name="db">OPTIONAL: If not readonly, provide database context</param>
		/// <returns></returns>
		internal static Users Entity(bool ReadOnly = true, DataContext db = null)
		{
			string email = GetEmail();

			if (ReadOnly == false && db == null)
			{
				throw new ArgumentNullException("DataContext cannot be null if it is not read-only");
			}

			if (ReadOnly == true)
				using (DataContext db1 = new DataContext())
				{
					Users user = (from u in db1.Users
								  where u.Email == email
								  select u).FirstOrDefault();
					if (user != null)
						return user;
					else
						throw new UserException("User not found");
				}
			else
			{
				Users user = (from u in db.Users
							  where u.Email == email
							  select u).FirstOrDefault();
				if (user != null)
					return user;
				else
					throw new UserException("User not found");
			}
		}
	}
}