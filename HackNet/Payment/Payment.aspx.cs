using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using Braintree;
using System.Configuration;

namespace HackNet.Payment
{
    public partial class Payment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void pkgConfirm_indexChange(object sender, EventArgs e)
        {
            pkgConfirm.Text = PackageList.SelectedItem.Text;
        }

        //Braintree stuff
        /*
            var gateway = new BraintreeGateway 
                    {
                        Environment = Braintree.Environment.SANDBOX,
                        MerchantId = ConfigurationManager.AppSettings["BraintreeMerchantId"],
                        PublicKey = ConfigurationManager.AppSettings["BraintreePublicKey"],
                        PrivateKey = ConfigurationManager.AppSettings["BraintreePrivateKey"],
                    };
        
        public class ClientTokenHandler : IHttpHandler
        {
            public void ProcessRequest(HttpContext context)
            {
                var clientToken = gateway.ClientToken.generate();
                context.Response.Write(clientToken);
            }
        }

        [HttpPost]
        public ActionResult CreatePurchase(FormCollection collection)
        {
            string nonceFromTheClient = collection["fake-valid-nonce"];
            // Use payment method nonce here
        }


        var request = new TransactionRequest
        {
            Amount = 10.00M,
            PaymentMethodNonce = nonceFromTheClient,
            Options = new TransactionOptionsRequest
            {
                SubmitForSettlement = true
            }
        };

        Result<Transaction> result = gateway.Transaction.Sale(request);
    }

    */
}
}