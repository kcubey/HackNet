using HackNet.Data;
using System;
using System.Data;
using System.Collections.Generic;
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

        }

        private DataTable LoadParts(int itemType)
        {

            List<Items> ilist = Data.Items.GetItems(itemType);
            string imageurlstring;
            string url;
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemName", typeof(string));
            dt.Columns.Add("ItemPic", typeof(string));
            foreach (Items i in ilist)
            {
                imageurlstring = Convert.ToBase64String(i.ItemPic, 0, i.ItemPic.Length);
                url = "data:image/png;base64," + imageurlstring;
                dt.Rows.Add(i.ItemName, url);
            }

            //ProcessList.DataSource = dt;
            //ProcessList.DataBind();
            return dt;
        }

        protected void btnAddListing_Click(object sender, EventArgs e)
        {
            MarketListings mklist = new MarketListings();
            mklist.ItemTitle = Listingtitle.Text;
            mklist.Description = ListingDescription.Text;
            mklist.Price = Int32.Parse(ListingPrice.Text);
            using (DataContext db = new DataContext())
            {
                db.MarketListings.Add(mklist);
                db.SaveChanges();
            }
        }
    }
}
