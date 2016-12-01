using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Braintree;

namespace HackNet.App_Code
{
    public interface IBraintreeConfiguration
    {
        IBraintreeGateway CreateGateway();
        string GetConfigurationSetting(string setting);
        IBraintreeGateway GetGateway();
    }
}