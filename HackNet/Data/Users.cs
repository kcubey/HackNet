using System;
using System.Text;
using System.Linq;
using System.Data.Entity.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HackNet.Security;

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

		// Foreign Key References
		public virtual Machines Machine { get; set; }

		public virtual UserKeyStore UserKeyStore { get; set; }

		public virtual ICollection<InventoryItem> Inventory { get; set; }

		public virtual ICollection<Messages> SentMessages { get; set; }

		public virtual ICollection<Messages> ReceivedMessages { get; set; }

		// Accessor for User Key Store
		internal UserKeyStore KeyStore
		{
			get
			{
				if (UserKeyStore == null)
				{
					UserKeyStore = new UserKeyStore
					{
						RsaPriv = new byte[0],
						RsaPub = new byte[0],
						TOTPSecret = null,
						UserId = this.UserID
					};
				}
				return this.UserKeyStore;
			}
		}

		// Simple method for password hash and salt updating
		internal void UpdatePassword(string newpassword)
		{
			Salt = Crypt.Instance.Generate(64);
			Hash = Crypt.Instance.Hash(Encoding.UTF8.GetBytes(newpassword), Salt);
		}

		// Find users by email or username
		internal static Users FindEmail(string email, DataContext db = null)
		{
			Users user;

			try
			{
				if (db != null)
					user = (from u in db.Users
						where u.Email == email
						select u).FirstOrDefault();
				else
					using (DataContext db1 = new DataContext())
						user = (from u in db1.Users
								where u.Email == email
								select u).FirstOrDefault();

			} catch (EntityCommandExecutionException)
			{
				throw new ConnectionException("Database link failure has occured");
			} catch (Exception)
			{
				return null;
			}

			// Returns NULL if no such user found
			return user;
		}

		internal static Users FindUsername(string username, DataContext db = null)
		{
			Users user;
			try
			{
				if (db != null)
					user = (from u in db.Users
						where u.UserName == username
						select u).FirstOrDefault();
				else
					using (DataContext db1 = new DataContext())
						user = (from u in db1.Users
								where u.UserName == username
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
	Unverified = -1,
	User = 0,
	Staff = 1,
	Admin = 2
}