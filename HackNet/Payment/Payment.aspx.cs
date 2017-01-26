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
            Debug.WriteLine(Form.ID);

            if (Session["packageId"] == null)
            {
                Response.Redirect("~/game/market1", true);
            }

            packageDetailsLbl.Text = "Package " + Session["packageId"].ToString() + " - $" + Session["packageprice"].ToString();

            //Braintree codes
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

            ClientScript.GetPostBackEventReference(this, string.Empty);

            if (!IsPostBack)
            {
                //Generate a client token
                clientToken = gateway.ClientToken.generate();
                Debug.WriteLine(clientToken);
            }
        }

        public void CancelClick(Object sender, EventArgs e)
        {
            Response.Redirect("~/game/market");
        }

        public void checkoutClick(Object sender, EventArgs e)
        {
            Debug.WriteLine("Enter checkoutclick event");
            price = Convert.ToDecimal(Session["packageprice"]);
            Debug.WriteLine("package price = " +price);

            //Get the nonce & device data from the client
            var nonce = "fake-valid-nonce";
            
            //var nonce = Request.Form["payment_method_nonce"];
            //var deviceData = Request.Form["device_data"];
            //Debug.WriteLine("nonce: " +nonce +" and device" +deviceData);
            Debug.WriteLine("nonce: " +nonce);
            
            //Create auth
            var request = new TransactionRequest
            {
                Amount = price,
                PaymentMethodNonce = nonce,
               // DeviceData = deviceData
            };
            Debug.WriteLine("transaction request made");

            //Send transaction request to server
            Result<Transaction> result = gateway.Transaction.Sale(request);
            Debug.WriteLine("transaction sent to server");

            if (result.IsSuccess())
            {
                Debug.WriteLine("is success");
                //Transaction is successful
                string transactionId = result.Target.Id.ToString();
                Session["transactionId"] = transactionId;

                //KTODO = Add in code to add items to invo

                Response.Redirect("~/payment/checkout");
            }
            else
            {
                Debug.WriteLine("is fail");
                //Something went wrong
                Response.Redirect("~/payment/retry");
            }
        }
    }
}