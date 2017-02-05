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
            int userid = int.Parse(e.CommandArgument.ToString());
            Cache["UserId"] = userid;
            List<Items> usrItemList = new List<Items>();
            Users u;
            using (DataContext db=new DataContext())
            {
                u = db.Users.Find(userid);
                usrItemList = ItemLogic.GetUserInvItems(u,-1,db); 
            }

            UserInvLbl.Text = u.UserName+"'s Inventory";
            ItemLogic.LoadUserManageInvetory(usrItemList, UserInvView);
        }
        protected void DeleteItem_Command(object sender, CommandEventArgs e)
        {
            if(Cache["UserId"] is int)
            {
                int itemid = int.Parse(e.CommandArgument.ToString());
                int userid = int.Parse(Cache["UserId"].ToString());
                if (ItemLogic.DeleteItemFromUserInv(userid, itemid))
                {
                    // Success in deleting
                    Response.Redirect("UserInventory.aspx", true);
                }
                else
                {
                    // Part in used cannot delete
                }
            }
            else
            {
                Response.Redirect("UserInventory.aspx", true);
            }
           
            
        }

        protected void AllItemsList_Load(object sender, EventArgs e)
        {
            List<Items> allItems=Data.Items.GetItems(-1) ;
            foreach(Items i in allItems)
            {
                AllItemsList.Items.Add(new ListItem(i.ItemName,i.ItemId.ToString()));
            }
        }

        protected void AddItemToUserInv_Click(object sender, EventArgs e)
        {
            InventoryItem invitem = new InventoryItem();
            invitem.UserId = int.Parse(UserIDTxtbox.Text);
            invitem.ItemId = int.Parse(AllItemsList.SelectedValue);
            invitem.Quantity = int.Parse(ItemQuantityTxtbox.Text);
            using (DataContext db=new DataContext())
            {
                db.InventoryItem.Add(invitem);
                db.SaveChanges();
            }
        }
    }
}