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
        public string clientToken;
        private BraintreeGateway gateway;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("enter pageload payment.aspx");

            Debug.WriteLine("enter pageload payment.aspxwrite form id");
            Form.ID = "checkout-form";
            packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() +" - $" + Session["packageprice"].ToString();
            //Braintree codes
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            gateway = new BraintreeGateway
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

            try
            {
                clientToken = gateway.ClientToken.generate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex)
            }

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

        protected void checkoutClick(Object sender, EventArgs e)
        {
            Debug.WriteLine("enter checkoutclick");

            //Get the nonce & device data from the client
            var nonce = Request.Form["payment_method_nonce"];
            var deviceData = Request.Form["device_data"];

            //Create auth
            var request = new TransactionRequest
            {
                Amount = (decimal)price / 100,
                PaymentMethodNonce = nonce,
                DeviceData = deviceData
            };

            //Send transaction request to server
            Result<Transaction> result = gateway.Transaction.Sale(request);

            if (result.IsSuccess())
            {
                //Transaction is successful
                string transactionId = string.Format(result.Target.Id);
                Session["transactionId"] = transactionId;
                Debug.WriteLine("successful");
                Response.Redirect("~/payment/checkout");
                Debug.WriteLine("redirect checkout");


            }
            else
            {
                //Something went wrong
                Response.Redirect("~/payment/retry");
                Debug.WriteLine("fail redirect");
            }

            Debug.WriteLine("exit checkoutclick");

        }
    }
}