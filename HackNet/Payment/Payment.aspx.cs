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


namespace HackNet.Payment
{
    public partial class Payment : System.Web.UI.Page
    {
        protected int price;

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("enter pageload payment.aspx");

            Debug.WriteLine("enter pageload payment.aspxwrite form id");
            Form.ID = "checkout-form";
            packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() +" - $" + Session["packageprice"].ToString();
            //Braintree codes
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            Debug.WriteLine("exit pageload payment");
        }

        public void getPkgPrice()
        {
            //below code to convert for gateway
            Debug.WriteLine("enter getpkgprice");
            price = Convert.ToInt32(Session["packagePrice"])*100;
            Debug.WriteLine("exit getpkgprice");
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/market");
        }

        public void checkoutClick(Object sender, EventArgs e)
        {
            Debug.WriteLine("enter checkoutclick");

            Debug.WriteLine("create gateway");
            BraintreeGateway Gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                PublicKey = ConfigurationManager.AppSettings["BraintreePublicKey"].ToString(),
                PrivateKey = ConfigurationManager.AppSettings["BraintreePrivateKey"].ToString(),
                MerchantId = ConfigurationManager.AppSettings["BraintreeMerchantId"].ToString(),

                /*
                PublicKey = "YOURPUBLICKEYHERE",
                PrivateKey = "YOURPRIVATEKEYHERE",
                MerchantId = "YOURMERCHANTIDHERE"
                */
            };
            Debug.WriteLine("gateway done");

            Debug.WriteLine("create transactionRequest");
            TransactionRequest transactionRequest = new TransactionRequest
            {
                Amount = (decimal)price/100,
                PaymentMethodNonce = "fake-valid-nonce",
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            Debug.WriteLine("transactionRequest done");

            Debug.WriteLine("submit transactionRequest");
            Result<Transaction> result = Gateway.Transaction.Sale(transactionRequest);
            Debug.WriteLine("transactionRequest submitted");

            Debug.WriteLine("check result");
            if (result.IsSuccess())
            {
                Debug.WriteLine("successful");
                Response.Redirect("~/payment/checkout");
                Debug.WriteLine("redirect checkout");
            }

            else
            {
                Debug.WriteLine("failed");
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }

                Debug.WriteLine("store message");
                Session["transactionError"] = errorMessages;
                Response.Redirect("~/payment/retry");
                Debug.WriteLine("fail redirect");

            }

            Debug.WriteLine("exit checkoutclick");

        }
    }
}