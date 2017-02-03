using HackNet.Data;
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

        protected void ViewUserInventory_Command(object sender, CommandEventArgs e)
        {

        }

        protected void AdminUsersView_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("User ID",typeof(string));
            dt.Columns.Add("Full Name", typeof(string));
            dt.Columns.Add("User Name",typeof(string));
            dt.Columns.Add("User Level",typeof(int));
            List<Users> allUserList = new List<Users>();
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
    }
}