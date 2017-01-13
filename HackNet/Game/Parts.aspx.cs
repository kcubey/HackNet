﻿using HackNet.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Parts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PartsList.DataSource = LoadInventory(-1);
            PartsList.DataBind();
        }

        private DataTable LoadInventory(int itemType)
        {

            List<Items> ilist = Data.Items.GetItems(itemType);
            foreach(Items i in ilist)
            {
                System.Diagnostics.Debug.WriteLine("Parts for parts: "+i.ItemName);
            }
            string imageurlstring;
            string url;
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemNo", typeof(int));
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemPic", typeof(string));
            foreach (Items i in ilist)
            {
                imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                url = "data:image/png;base64," + imageurlstring;
                dt.Rows.Add(i.ItemId,i.ItemName,url);
            }

            //ProcessList.DataSource = dt;
            //ProcessList.DataBind();
            return dt;
        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.ItemName = ItemName.Text;
            item.ItemType = (ItemType)Int32.Parse(ItemTypeList.SelectedItem.Value);

            Stream strm = UploadPhoto.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            item.ItemPic = br.ReadBytes((int)strm.Length);
            item.ItemDesc = ItemDesc.Text;
            item.ItemPrice = Int32.Parse(ItemPrice.Text);
            item.ItemBonus = Int32.Parse(ItemStat.Text);
            using (DataContext db = new DataContext())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }


        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            MarketListings mklist = new MarketListings();

            using (DataContext db = new DataContext())
            {
                db.MarketListings.Add(mklist);
                db.SaveChanges();
            }
        }


        protected void ViewMore_Command(object sender, CommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            Items item = Data.Items.GetItem(id);
            Session["Item"] = item;
            Server.Transfer("PartsInfo.aspx", true);
        }
    }
}
