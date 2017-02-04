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

        public static void StoreUserIP(Users u, string IP)
        {
            using(DataContext db=new DataContext())
            {
                UserIPList uip = new UserIPList();
                uip.UserId = u.UserID;
                uip.UserIPStored = IP;
                db.UserIPList.Add(uip);
                db.SaveChanges();
            }
            
        }
        
        internal static bool CheckUserIPList(string IP, Users u, DataContext db)
        {
            var query = from uip in db.UserIPList where uip.UserId == u.UserID select uip;

            List<UserIPList> uipList = query.ToList();
            var match = uipList.FirstOrDefault(IPToChk =>IPToChk.UserIPStored.Contains(IP));

            if (match == null)
            {
                StoreUserIP(u, IP);
                return true;
            }else
            {
                return false;
            }     
        }
    }
}