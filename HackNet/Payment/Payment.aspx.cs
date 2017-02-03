using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Braintree;
using System.Configuration;
using System.Net;
using System.Diagnostics;
using HackNet.Data;
using HackNet.Security;

namespace HackNet.Payment
{
    public partial class Payment : System.Web.UI.Page
    {
        protected Decimal price;
        protected int pkgId;
        protected int pkgItemId;
        protected string pkgItemName;
        protected int pkgItemQuantity;
        protected double pkgPrice;

        protected BraintreeGateway gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            PublicKey = ConfigurationManager.AppSettings["BraintreePublicKey"].ToString(),
            PrivateKey = ConfigurationManager.AppSettings["BraintreePrivateKey"].ToString(),
            MerchantId = ConfigurationManager.AppSettings["BraintreeMerchantId"].ToString(),
        };

        public string clientToken;

        protected void Page_Load(object sender, EventArgs e)
        {
            Form.ID = "checkout-form";
            try
            {
                Pack pkg = Session["pkg"] as Pack;
                PackageItems pkgItems = Session["pkgItems"] as PackageItems;
                if (pkg == null)
                {
                    Response.Redirect("~/game/currency", true);
                }
                pkgId = pkg.PackageId;
                pkgPrice = pkg.Price;
                pkgItemQuantity = pkgItems.Quantity;
                pkgItemId = pkgItems.ItemId;

                //packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() + " - $" + Session["packageprice"].ToString();
                packageDetailsLbl.Text = "Package " + pkgId + " - $" + pkgPrice;
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }

            //Braintree codes
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (!IsPostBack)
            {
                //Generate client token
                clientToken = gateway.ClientToken.generate();
            }

            if (IsPostBack)
            {
                checkoutClickA();
                //KTODO minor: Change to modal then checkout
            }

        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/market", true);
        }

        public void checkoutClick(Object sender, EventArgs e)
        {
            try
            {
                price = Convert.ToDecimal(pkgPrice);
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }

            //set nonce
            var nonce = "fake-valid-nonce";

            //create transaction request
            var request = new TransactionRequest
            {
                Amount = price,
                PaymentMethodNonce = nonce,
            };

            //send transaction request to server
            Result<Transaction> result = gateway.Transaction.Sale(request);

            //transaction is successful
            if (result.IsSuccess())
            {
                try
                {
                    //get transaction id
                    string transactionId = result.Target.Id.ToString();
                    Session["transactionId"] = transactionId;

                    Items i = HackNet.Data.Items.GetItem(pkgItemId);
                    pkgItemName = i.ItemName.ToString();
                    int addItemQty = pkgItemQuantity;

                    using (DataContext db = new DataContext())
                    {
                        Users u = CurrentUser.Entity(false, db);
                        if (pkgItemName == "Buck")
                        {
                            int currBuck = u.ByteDollars;
                            int newBuck = currBuck + addItemQty;
                            u.ByteDollars = newBuck;
                        }
                        else if (pkgItemName == "Coin")
                        {
                            int currCoin = u.Coins;
                            int newCoin = currCoin + addItemQty;
                            u.Coins = newCoin;
                        }
                        else
                        {
                            Game.Class.ItemLogic.AddItemToInventory(u, pkgItemId, addItemQty);
                        }
                        
                        db.SaveChanges();
                        //KTODO = Check items/bucks are added
                    }
                }
                catch
                {
                    Response.Redirect("~/game/currency", true);
                }

                Response.Redirect("~/payment/checkout", true);
            }
            //transaction fail
            else
            {
                Response.Redirect("~/payment/retry", true);
            }
        }


        public void checkoutClickA()
        {
            try
            {
                price = Convert.ToDecimal(pkgPrice);
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }

            var nonce = "fake-valid-nonce";

            var request = new TransactionRequest
            {
                Amount = price,
                PaymentMethodNonce = nonce,
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                string transactionId = result.Target.Id.ToString();
                Session["transactionId"] = transactionId;

                int addBuck = Convert.ToInt32(Session["itemQuantity"]);

                using (DataContext db = new DataContext())
                {
                    Users u = CurrentUser.Entity(false, db);
                    int currBuck = u.ByteDollars;
                    int newBuck = currBuck + addBuck;
                    u.ByteDollars = newBuck;

                    db.SaveChanges();
                    //dbBuck = u.ByteDollars;

                    Response.Redirect("~/payment/checkout", true);
                }
            }
            else
            {
                Response.Redirect("~/payment/retry", true);
            }
        }
    }
}