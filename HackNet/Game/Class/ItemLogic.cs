using HackNet.Data;
using HackNet.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace HackNet.Game.Class
{
    public class ItemLogic
    {
        /// <summary>
        /// Get User owned Items
        /// </summary>
        /// <param name="user"></param>
        /// <param name="itemType"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        internal static List<Items> GetUserInvItems(Users user, int itemType, DataContext db)
        {
            var query = from inv in db.InventoryItem where inv.UserId == user.UserID select inv;

            // For debugging Atm
            List<InventoryItem> invlist = query.ToList();
            List<Items> itmList = new List<Items>();
            foreach (InventoryItem inv in invlist)
            {
                for (int i = 0; i < inv.Quantity; i++)
                {
                    itmList.Add(Items.GetItem(inv.ItemId));
                }
            }
            if (itemType != -1)
            {
                itmList.RemoveAll(element => element.ItemType != (ItemType)itemType);
            }
            return itmList;

        }

        /// <summary>
        /// Load Item into DataList for Inventory
        /// </summary>
        /// <param name="dl"></param>
        /// <param name="itmlist"></param>
        /// <param name="itemType"></param>
        internal static void LoadInventory(DataList dl, List<Items> itmlist, int itemType)
        {
            if (itmlist.Count != 0)
            {
                string imageurlstring;
                string url;
                DataTable dt = new DataTable();
                dt.Columns.Add("ItemName", typeof(string));
                dt.Columns.Add("ItemPic", typeof(string));
                dt.Columns.Add("ItemId", typeof(int));
                foreach (Items i in itmlist)
                {
                    if (itemType != -1)
                    {
                        if (i.ItemType == (ItemType)itemType)
                        {
                            imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                            url = "data:image/png;base64," + imageurlstring;

                            dt.Rows.Add(i.ItemName, url, i.ItemId);
                        }
                    }
                    else
                    {
                        imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                        url = "data:image/png;base64," + imageurlstring;

                        dt.Rows.Add(i.ItemName, url, i.ItemId);
                    }
                }
                dl.DataSource = dt;
                dl.DataBind();
            }
            else
            {
                dl.DataSource = null;
                dl.DataBind();
            }

        }
        
        /// <summary>
        /// Load Users inventory for admin to manage
        /// </summary>
        /// <param name="usrItemList"></param>
        /// <param name="gv"></param>
        public static void LoadUserManageInvetory(List<Items> usrItemList,GridView gv)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemID", typeof(int));
            dt.Columns.Add("Item Name", typeof(string));
            dt.Columns.Add("Item Bonus", typeof(int));
            if (usrItemList.Count != 0)
            {
                foreach (Items i in usrItemList)
                {
                    dt.Rows.Add(i.ItemId, i.ItemName, i.ItemBonus);
                }
                gv.DataSource = dt;
                gv.DataBind();
            }else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
           
        }
        
        public static bool DeleteItemFromUserInv(int UserID,int ItemID)
        {
            
            using(DataContext db=new DataContext())
            {
                if (!MachineLogic.CheckInstalledParts(UserID, ItemID,db))
                {
                    InventoryItem invitem = db.InventoryItem.Where(x => x.UserId == UserID && x.ItemId == ItemID).FirstOrDefault();
                    db.InventoryItem.Remove(invitem);
                    db.SaveChanges();
                    return true;
                }else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Get the Default Parts
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        internal static List<Items> GetDefaultParts(DataContext db)
        {
            List<Items> itmlist;
            var query = from i in db.Items where i.ItemId == 11 || i.ItemId == 8 || i.ItemId == 9 || i.ItemId == 10 select i;
            itmlist = query.ToList();
            return itmlist;
        }

        /// <summary>
        /// Store Default Parts into User Inventory
        /// </summary>
        /// <param name="db"></param>
        /// <param name="userid"></param>
        internal static void StoreDefaultParts(DataContext db, int userid)
        {
            InventoryItem inv;
            foreach (Items i in GetDefaultParts(db))
            {
                inv = new InventoryItem(userid, i.ItemId, 1);
                db.InventoryItem.Add(inv);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Add item into user invetory
        /// </summary>
        /// <param name="user"></param>
        /// <param name="itemid"></param>
        /// <param name="quantity"></param>
        internal static void AddItemToInventory(Users user, int itemid, int quantity = 1)
        {
            using (DataContext db = new DataContext())
            {
                InventoryItem invitem;
                if (CheckInventoryItem(db, user, itemid, out invitem))
                {
                    invitem.Quantity += quantity;
                    db.SaveChanges();
                }
                else
                {
                    invitem = new InventoryItem();
                    invitem.ItemId = itemid;
                    invitem.UserId = user.UserID;
                    invitem.Quantity = quantity;
                    db.InventoryItem.Add(invitem);
                    db.SaveChanges();
                }
            }
        }

        // Check if item is in inventory
        public static bool CheckInventoryItem(DataContext db, Users user, int itemid, out InventoryItem invItem)
        {

            invItem = (from i in db.InventoryItem where i.UserId == user.UserID && i.ItemId == itemid select i).FirstOrDefault();
            if (invItem == null)
            {
                return false;

            }
            else
            {
                return true;
            }
        }
        
        /// <summary>
        /// Using probability to get a random Item for reward
        /// </summary>
        /// <param name="normstat"></param>
        /// <param name="rarestat"></param>
        /// <param name="probability"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static Items GetItemsForRewards(int normstat, int rarestat, int probability, DataContext db)
        {
            List<Items> normList = (from i in db.Items where i.ItemBonus < normstat && i.ItemBonus > 0 select i).ToList();
            List<Items> rareList = (from i in db.Items where i.ItemBonus <= rarestat && i.ItemBonus > normstat select i).ToList();

            Random R = new Random();
            int C = R.Next(1, 101);
            System.Diagnostics.Debug.WriteLine("Calculated: " + C);
            if (C < probability)
            {
                int index = R.Next(0, rareList.Count);
                return rareList[index];
            }
            else
            {
                int index = R.Next(0, normList.Count);
                return normList[index];
            }
        }



        /// <summary>
        /// Get a reward list for missions
        /// </summary>
        /// <param name="db"></param>
        /// <param name="misLevelRequire"></param>
        /// <param name="m"></param>
        internal static Items GetRewardForMis(RecommendLevel misLevelRequire, Machines m)
        {

            int probability = MachineLogic.CalculateMachineLuck(m);
            using (DataContext db = new DataContext())
            {
                if (misLevelRequire == 0)
                {
                    Items Reward = GetItemsForRewards(10, 20, probability, db);
                    return Reward;
                }
                else if (misLevelRequire == (RecommendLevel)1)
                {
                    Items Reward = GetItemsForRewards(11, 25, probability, db);
                    return Reward;


                }
                else if (misLevelRequire == (RecommendLevel)2)
                {
                    Items Reward = GetItemsForRewards(12, 30, probability, db);
                    return Reward;
                }
                else if (misLevelRequire == (RecommendLevel)3)
                {
                    Items Reward = GetItemsForRewards(13, 35, probability, db);
                    return Reward;
                }
                else
                {
                    return null;
                }
            }
        }


    }
}