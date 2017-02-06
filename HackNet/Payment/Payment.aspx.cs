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
        protected string pkgDetails;
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

                pkgDetails = "Package " + pkgId + " - $" + pkgPrice;

                packageDetailsLbl.Text = pkgDetails;
                modalPackageDetails.Text = pkgDetails;
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
                DisplayModal();
            }
        }

        protected void DisplayModal()
        {
            if(Session["packageId"] != null && Session["packagePrice"] != null && Session["itemQuantity"] != null
                    && Session["itemId"] != null && Session["itemName"] != null)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmPayModal", "showConfirmPayModal()", true);
            }
            else
            {
                Response.Redirect("~/game/currency", true);
            }
        }

        protected void confirmBtn_Click(Object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ConfirmPayModal", "showConfirmPayModal()", true);
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/currency", true);
            Session.Abandon();
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
    }
}