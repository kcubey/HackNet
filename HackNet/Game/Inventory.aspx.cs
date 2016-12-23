using HackNet.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HackNet.Game
{
    public partial class Inventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddItem_Click(object sender, EventArgs e)
        {
            Items item = new Items();
            item.ItemName = ItemName.Text;
            item.ItemType = (ItemType)Int32.Parse(ItemTypeList.SelectedItem.Value);

            Stream strm = UploadPhoto.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(strm);
            item.ItemPic= br.ReadBytes((int)strm.Length);
            item.ItemDesc = ItemDesc.Text;
            item.ItemPrice = Int32.Parse(ItemPrice.Text);
            item.ItemBonus = Int32.Parse(ItemStat.Text);
            using(DataContext db=new DataContext())
            {
                db.Items.Add(item);
                db.SaveChanges();
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (UploadPhoto.HasFile)
            {
                Stream strm = UploadPhoto.PostedFile.InputStream;
                BinaryReader br = new BinaryReader(strm);
                // this is the thing u need to throw into the database
                byte[] imageByte = br.ReadBytes((int)strm.Length);

                string base64string = Convert.ToBase64String(imageByte, 0, imageByte.Length);
                imgViewFile.ImageUrl = "data:image/png;base64," + base64string;

            }
        }
    }
}