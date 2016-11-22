using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackNet.Security {
	public class User {

		internal int Id { get; set; }
		internal string UserName { get; set; }
		internal string FullName { get; set; }
		internal string Email { get; set; }
		internal DateTime BirthDate { get; set; }
		internal DateTime Registered { get; set; }
		internal DateTime LastLogin { get; set; }
		internal int Coins { get; set; }
		internal int ByteDollars { get; set; }
		internal int Experience { get; set; }
		internal int AccessLevel { get; set; }

		internal User(int userid) {

		}

		internal User(string email) {

		}

	}
}