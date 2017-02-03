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
                packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() + " - $" + Session["packageprice"].ToString();
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
                //KTODO: Change to modal then checkout
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
                price = Convert.ToDecimal(Session["packageprice"]);
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

                    int addBuck = Convert.ToInt32(Session["itemQuantity"]);

                    using (DataContext db = new DataContext())
                    {
                        Users u = CurrentUser.Entity(false, db);
                        int currBuck = u.ByteDollars;
                        int newBuck = currBuck + addBuck;
                        u.ByteDollars = newBuck;

                        db.SaveChanges();
                        //dbBuck = u.ByteDollars;
                    }
                }
                catch
                {
                    Response.Redirect("~/game/currency", true);
                }
                //KTODO = Add in code to add items to invo

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
                price = Convert.ToDecimal(Session["packageprice"]);
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
            else
            {
                Response.Redirect("~/payment/retry", true);
            }
        }
    }
}