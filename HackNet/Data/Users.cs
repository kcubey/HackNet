using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HackNet.Security;
using System.Text;
using System.Data.Entity.Core;

namespace HackNet.Data
{
	public partial class Users
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserID { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[Display(Name = "User Name")]
		[StringLength(15, ErrorMessage = "Username must be between 4 to 15 characters", MinimumLength = 4)]
		public string UserName { get; set; }

		[Required]
		[Display(Name = "Full Name")]
		[StringLength(50, ErrorMessage = "Full name must be between 5 to 50 characters", MinimumLength = 5)]
		public string FullName { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[Display(Name = "Email address")]
		[StringLength(70)]
		[EmailAddress(ErrorMessage = "Ensure that the email address is properly entered!")]
		public string Email { get; set; }

		public byte[] Hash { get; set; }

		public byte[] Salt { get; set; }

		[DataType(DataType.Date)]
		public DateTime BirthDate { get; set; }

		public DateTime Registered { get; set; }

		public DateTime LastLogin { get; set; }

		public int Coins { get; set; }

		public int ByteDollars { get; set; }

		public AccessLevel AccessLevel { get; set; }

		public string TOTPSecret { get; set; }

		// Foreign Key References
		public virtual Machines Machine { get; set; }

		public virtual UserKeyStore UserKeyStore { get; set; }

		public virtual ICollection<InventoryItem> Inventory { get; set; }

		public virtual ICollection<Messages> SentMessages { get; set; }

		public virtual ICollection<Messages> ReceivedMessages { get; set; }

		// Controller-ish Business Logic-ish Layer
		internal bool UpdatePassword(string newpassword, string oldpassword = null)
		{
			using (Authenticate a = new Authenticate())
			{
				if (oldpassword != null && (a.ValidateLogin(Email, oldpassword) != Authenticate.AuthResult.Success))
					return false;
				Salt = Authenticate.Generate(64);
				Hash = a.Hash(Encoding.UTF8.GetBytes(newpassword), Salt);
				return true;
			}
		}

		// Static useful methods
		internal static Users FindUser(string email, DataContext db)
		{
			Users user;
			try
			{
				user = (from u in db.Users
						where u.Email == email
						select u).FirstOrDefault();
			} catch (EntityCommandExecutionException)
			{
				throw new ConnectionException("Database link failure has occured");
			}
			
			// Returns NULL if no such user found
			return user;
		}
	}
}

public enum AccessLevel
{
	Banned = -1,
	User = 0,
	Staff = 1,
	Admin = 2
}