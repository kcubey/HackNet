using HackNet.Data;
using HackNet.Game.Class;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Admin
{
    public partial class UserInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

       

        protected void AdminUsersView_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("UserID",typeof(string));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("User Name",typeof(string));
            dt.Columns.Add("User Level",typeof(int));
            List<Users> allUserList = new List<Users>();
            using(DataContext db=new DataContext())
            {
                allUserList = (from u in db.Users select u).ToList();
            }
            if (allUserList.Count != 0)
            {
                foreach (Users u in allUserList)
                {
                    dt.Rows.Add(u.UserID, u.FullName, u.UserName, u.Level.GetLevel());
                }
                AdminUsersView.DataSource = dt;
                AdminUsersView.DataBind();
            }else
            {
                AdminUsersView.DataSource = null;
                AdminUsersView.DataBind();
            }
        }
        protected void ViewUserInventory_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            List<Items> usrItemList = new List<Items>();
            using (DataContext db=new DataContext())
            {
                /*
                Users u = Users.FindByEmail();
                List<Items> usrItemList = ItemLogic.GetUserInvItems(u,-1,db);

                */
            }
            foreach(Items i in usrItemList)
            {
                DataTable dt = new DataTable();

            }
        }
        protected void DeleteItem_Command(object sender, CommandEventArgs e)
        {

        }
    }
}