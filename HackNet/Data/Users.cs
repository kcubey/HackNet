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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Number")]
        public int UserID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FullName { get; set; }

        public string Email { get; set; }
   
        public string Hash { get; set; }

        public string Salt { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime Registered { get; set; }

        public DateTime LastLogin { get; set; }

        public int Coins { get; set; }

        public int ByteDollars { get; set; }

        public AccessLevel AccessLevel { get; set;  }
    }

    public enum AccessLevel
    {
        Banned = -1,
        User = 0,
        Staff = 1,
        Admin = 2
    }
}