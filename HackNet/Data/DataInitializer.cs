using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace HackNet.Data
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var Users = new List<Users>
            {
                new Users{
                    UserName ="MasterGamer",
                    FullName = "Wen Liang",
                    Email = "wugglelord@gmail.com",
                    Hash = "SomeHashHere",
                    Salt = "SomeSaltHere",
                    BirthDate = DateTime.Parse("1998-03-17"),
                    Registered = DateTime.Parse("2016-10-10"),
                    LastLogin = DateTime.Parse("2016-10-11")
                }
            };
            Users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

        }
    }
}