using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Braintree;
using System.Configuration;

//Braintree file

namespace HackNet.App_Code
{
    public class BraintreeConfiguration : IBraintreeConfiguration
    {
        /*
        BraintreeGateway gateway = new BraintreeGateway
        {
            Environment = Braintree.Environment.SANDBOX,
            MerchantId = "697v7np6yxddsf8r",
            PublicKey = "vmfb4gw9bf6nn759",
            PrivateKey = "654f35d55d2b2b246a907333faddc204"
        };
        */

        public string Environment { get; set; }
        public string MerchantId { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        private IBraintreeGateway BraintreeGateway { get; set; }

        public IBraintreeGateway CreateGateway()
        {
            /*
            Environment = System.Environment.GetEnvironmentVariable("Braintree.Environment.SANDBOX");
            MerchantId = System.Environment.GetEnvironmentVariable("697v7np6yxddsf8r");
            PublicKey = System.Environment.GetEnvironmentVariable("vmfb4gw9bf6nn759");
            PrivateKey = System.Environment.GetEnvironmentVariable("654f35d55d2b2b246a907333faddc204");
            */

            Environment = System.Environment.GetEnvironmentVariable("BraintreeEnvironment");
            MerchantId = System.Environment.GetEnvironmentVariable("BraintreeMerchantId");
            PublicKey = System.Environment.GetEnvironmentVariable("BraintreePublicKey");
            PrivateKey = System.Environment.GetEnvironmentVariable("BraintreePrivateKey");

            if (MerchantId == null || PublicKey == null || PrivateKey == null)
            {
                Environment = GetConfigurationSetting("BraintreeEnvironment");
                MerchantId = GetConfigurationSetting("BraintreeMerchantId");
                PublicKey = GetConfigurationSetting("BraintreePublicKey");
                PrivateKey = GetConfigurationSetting("BraintreePrivateKey");
            }

            return new BraintreeGateway(Environment, MerchantId, PublicKey, PrivateKey);
        }

        public string GetConfigurationSetting(string setting)
        {
            return ConfigurationManager.AppSettings[setting];
        }

        public IBraintreeGateway GetGateway()
        {
            if (BraintreeGateway == null)
            {
                BraintreeGateway = CreateGateway();
            }

            return BraintreeGateway;
        }
    }
}
