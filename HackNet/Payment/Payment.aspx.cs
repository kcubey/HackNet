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
        protected Decimal tPrice;
        protected int pkgId;
        protected int pkgItemId;
        protected string pkgItemName;
        protected int pkgItemQuantity;
        protected decimal pkgPrice;

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
            Debug.WriteLine("enter payment pageload");
            Form.ID = "checkout-form";

            if (Session["packageId"]!=null && Session["packagePrice"]!=null && Session["itemQuantity"]!=null
                    && Session["itemId"]!=null && Session["itemName"]!=null)
            {
                Debug.WriteLine("enter if else");
                pkgId = (int)Session["packageId"];
                pkgPrice = (decimal)Session["packagePrice"];
                pkgItemQuantity = (int)Session["itemQuantity"];
                pkgItemId = (int)Session["itemId"];
                pkgItemName = (string)Session["itemName"];

                packageDetailsLbl.Text = "Package " + pkgId + " - $" + pkgPrice;
            }
            else
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
                tPrice = Convert.ToDecimal(pkgPrice);
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
                Amount = tPrice,
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
                Debug.WriteLine("enter try/catch checkpoutA");
                tPrice = Convert.ToDecimal(pkgPrice);
            }
            catch
            {
                Response.Redirect("~/game/currency", true);
            }


            //set nonce
            var nonce = "fake-valid-nonce";
            Debug.WriteLine(" set nonce");

            //create transaction request
            var request = new TransactionRequest
            {
                Amount = tPrice,
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
                    
                    using (DataContext db = new DataContext())
                    {
                        Users u = CurrentUser.Entity(false, db);
                        if (pkgItemName == "Buck")
                        {
                            int currBuck = u.ByteDollars;
                            int newBuck = currBuck + pkgItemQuantity;
                            u.ByteDollars = newBuck;
                        }
                        else if (pkgItemName == "Coin")
                        {
                            int currCoin = u.Coins;
                            int newCoin = currCoin + pkgItemQuantity;
                            u.Coins = newCoin;
                        }
                        else
                        {
                            Game.Class.ItemLogic.AddItemToInventory(u, pkgItemId, pkgItemQuantity);
                        }

                        db.SaveChanges();
                    }
                }
                catch
                {
                Debug.WriteLine("catch @ issuccess");
                    Response.Redirect("~/game/currency", true);
                }

                Debug.WriteLine("goto checkout");
                Response.Redirect("~/payment/checkout", true);
            }
            //transaction fail
            else
            {
                Response.Redirect("~/payment/retry", true);
            }
        }
    }
}