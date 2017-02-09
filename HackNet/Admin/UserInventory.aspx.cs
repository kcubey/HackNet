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
            List<Items> allItems = Data.Items.GetItems(-1);
            foreach (Items i in allItems)
            {
                AllItemsList.Items.Add(new ListItem(i.ItemName, i.ItemId.ToString()));
            }
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
                    OutComeLbl.Text ="Successful Delete of Item";
                    WarningLbl.Visible = false;
                    WarningMessageLbl.Text = "Item Deleted from user inventory.";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UserInvenModal", "ShowPopUp();", true);
                    Response.Redirect("UserInventory.aspx", true);
                }
                else
                {
                    OutComeLbl.Text = "Delete Error";
                    WarningMessageLbl.Text = "Item cannot be deleted from user's inventory as part is used in machine";
                    // Part in used cannot delete
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "UserInvenModal", "ShowPopUp();", true);
                }
            }
            else
            {
                Response.Redirect("UserInventory.aspx", true);
            }
           
            
        }
        
        protected void AddItemToUserInv_Click(object sender, EventArgs e)
        {
            int userid = int.Parse(Cache["UserId"].ToString());
            InventoryItem invItem;
            using (DataContext db=new DataContext())
            {
                if(ItemLogic.CheckInventoryItem(db, userid, int.Parse(AllItemsList.SelectedValue),out invItem))
                {
                    invItem.Quantity = invItem.Quantity + 1;
                    db.SaveChanges();
                }
                else
                {
                    invItem=new InventoryItem();
                    invItem.UserId = userid;
                    invItem.Quantity = 1;
                    db.InventoryItem.Add(invItem);
                    db.SaveChanges();
                }
                Response.Redirect("UserInventory.aspx", true);
            }
        }
    }
}