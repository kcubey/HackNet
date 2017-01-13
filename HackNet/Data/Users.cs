using System;
using System.Text;
using System.Linq;
using System.Data.Entity.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using HackNet.Security;
using HackNet.Data;

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

		[Required]
		public int TotalExp { get; set; }

		[Required]
		public int Coins { get; set; }

		[Required]
		public int ByteDollars { get; set; }

		public AccessLevel AccessLevel { get; set; }

		// Foreign Key References
		public Machines Machine { get; set; }

		public UserKeyStore UserKeyStore { get; set; }

		public virtual ICollection<InventoryItem> Inventory { get; set; }

		public virtual ICollection<Messages> SentMessages { get; set; }

		public virtual ICollection<Messages> ReceivedMessages { get; set; }

		// Simple method for password hash and salt updating
		internal void UpdatePassword(string newpassword)
		{
			Salt = Crypt.Instance.Generate(64);
			Hash = Crypt.Instance.Hash(Encoding.UTF8.GetBytes(newpassword), Salt);
		}

		internal Level Level
		{
			get
			{
				return new Level(this);
			}
		}


		// Find users by email or username
		internal static Users FindByEmail(string email, DataContext db = null)
		{
			Users user;

			try
			{
				if (db != null)
					user = db.Users.Where(x => x.Email == email).FirstOrDefault();
				else
					using (DataContext ldb = new DataContext())
						user = ldb.Users.Where(x => x.Email == email).FirstOrDefault();


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

		internal static Users FindByUsername(string username, DataContext db = null)
		{
			Users user;
			try
			{
				if (db != null)
					user = db.Users.Where(x => x.UserName == username).FirstOrDefault();
				else
					using (DataContext ldb = new DataContext())
						user = ldb.Users.Where(x => x.UserName == username).FirstOrDefault();

			} catch (EntityCommandExecutionException)
			{
				throw new ConnectionException("Database link failure has occured");
			}

			// Returns NULL if no such user found
			return user;
		}
	}
}

internal class Level
{
	Users player;

	// Constructor
	internal Level(Users player)
	{
		this.player = player;
	}

	/// <summary>
	/// Gets current level of the user
	/// </summary>
	internal int GetLevel() {
		int totalexp = player.TotalExp;
		if (totalexp >= TotalExpNeededFor(40)) // MAX LEVEL 40
			return 40;
		for (int i = 1; i < 40; i++)
			if (totalexp >= TotalExpNeededFor(i) && totalexp < TotalExpNeededFor(i + 1))
				return i;
		return -1; // If all else fails
	}

	/// <summary>
	/// Cumulative EXP needed to reach next level (ignores progress in current level)
	/// </summary>
	internal int TotalForNextLevel()
	{
		return TotalExpNeededFor(GetLevel() + 1);
	}

	/// <summary>
	/// Amount of EXP needed for THIS user to reach next level
	/// </summary>
	internal int AmountToReachNextLevel()
	{
		return TotalForNextLevel() - player.TotalExp;
	}

	/// <summary>
	/// STATIC METHOD: Total exp needed for a given level
	/// </summary>
	internal static int TotalExpNeededFor(int lvl)
	{
		if (lvl <= 1)
			return 0;
		else
			return (int)(TotalExpNeededFor(--lvl) * 1.4 + 10);
	}
}

public enum AccessLevel
{
	Unverified = -1,
	User = 0,
	Staff = 1,
	Admin = 2
}

