using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
    public class Users
    {
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

		[Required]
		[Display(Name = "User Name")]
		[StringLength(15, ErrorMessage = "Username must be between 4 to 15 characters", MinimumLength = 4)]
        public string UserName { get; set; }

		[Required]
		[Display(Name = "Full Name")]
		[StringLength(50, ErrorMessage = "Full name must be between 5 to 50 characters", MinimumLength = 5)]
        public string FullName { get; set; }

		[Required]
		[Display(Name = "Email address")]
		[EmailAddress(ErrorMessage = "Ensure that the email address is properly entered!")]
		public string Email { get; set; }
   
        public string Hash { get; set; }

        public string Salt { get; set; }

		[DataType(DataType.Date)]
		public DateTime BirthDay { get; set; }

        public DateTime Registered { get; set; }

        public DateTime LastLogin { get; set; }

        public int Coins { get; set; }

        public int ByteDollars { get; set; }

        public AccessLevel AccessLevel { get; set;  }

		public bool isAdmin { get; set; }


    }

    public enum AccessLevel
    {
        Banned = -1,
        User = 0,
        Staff = 1,
        Admin = 2
    }
}