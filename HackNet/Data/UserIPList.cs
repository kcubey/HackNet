using HackNet.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

namespace HackNet.Data
{
    public class UserIPList
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string UserIPStored { get; set; }


        internal static bool CheckUserIPList(string IP,DataContext db)
        {
            Users user = Authenticate.GetCurrentUser();

            var query = from uip in db.UserIPList where uip.UserId == user.UserID select uip;

            List<UserIPList> uipList = query.ToList();

            foreach(UserIPList uip in uipList)
            {
                if (!uip.UserIPStored.Equals(IP))
                {
                    return false;
                }
            }
            return true;          
        }
    }
}