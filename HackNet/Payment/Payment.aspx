<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="HackNet.Payment.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/payment/backend/paymentCSS.css" />

    <h1>Confirm your Details</h1><hr />
    <asp:Label ID="warning" runat="server" Text="* WARNING *" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
    <p>You have chosen to use REAL currency to pay for premium items.
        <br />
        If you have NOT chosen to do so, <asp:LinkButton ID="backButton" runat="server" postbackurl="~/Game/Market.aspx">click to go back</asp:LinkButton>
    </p>
    <h3>Payment Details</h3>
        <asp:DropDownList ID="PackageList" runat="server" ForeColor="Black" 
            AutoPostBack="true" onselectedindexchanged="pkgConfirm_indexChange" ClientIDMode="Static">
            <asp:ListItem Text="Package A - SGD$10" Value="0"></asp:ListItem>
            <asp:ListItem Text="Package B - SGD$20" Value="1"></asp:ListItem>
            <asp:ListItem Text="Package C - SGD$30" Value="2"></asp:ListItem>
            <asp:ListItem Text="Package D - SGD$40" Value="3"></asp:ListItem>
        </asp:DropDownList>
    <br />
    <asp:Label ID="pkgConfirm" runat="server" ForeColor="Black" ClientIDMode="Static"></asp:Label>
   
    <br /><br />
    <h3>Pay by Paypal or Credit Card</h3>
    <hr /><p>For Paypal, click on the Paypal button. For Credit Card, enter a valid card number and expiration date.</p>
    <div class="dropinBox">
        <div id="payment-form"></div>
    </div>

<!--
    <h3>JS v3</h3>

    <h6>added from v2</h6>
    <button id="braintree-paypal-button" class="paypal is-active " title="Pay with PayPal">
      <span class="paypal-button-logo"></span>
  </button>

  <br /><label for="card-number">Card Number: </label>
  <div class="hosted-field" id="card-number"></div>

  <br /><label for="cvv">CVV: </label>
  <div class="hosted-field" id="cvv"></div>

  <br /><label for="expiration-date">Expiration Date:</label>
  <div class="hosted-field" id="expiration-date"></div>
    -->
  <input type="hidden" name="payment-method-nonce">

    <br />
    <asp:LinkButton runat="server" type="submit" CssClass="btn btn-success" Text="Checkout" Font-Size="Large" PostBackUrl="~/Payment/Checkout.aspx"></asp:LinkButton>

    <!-- JSv2-->
    <!--<script src="https://js.braintreegateway.com/js/braintree-2.30.0.min.js"></script>-->
    <script src="/Payment/backend/JSv2.js" lang="javascript" type="text/javascript"></script>

<!--
    <!-- JS v3
    <!-- Load the Client component. 
    <!--<script src="https://js.braintreegateway.com/web/3.6.0/js/client.min.js"></script>
    <script src="/Payment/backend/JSv3Client.js" lang="javascript" type="text/javascript"></script>

    <!-- Load the Hosted Fields component. 
    <!--<script src="https://js.braintreegateway.com/web/3.6.0/js/hosted-fields.min.js"></script>
    <script src="/Payment/backend/JSv3HostedFields.js" lang="javascript" type="text/javascript"></script>
-->
    <!-- all-->
    <script src="/Payment/backend/paymentJS.js" lang="javascript" type="text/javascript"></script>

</asp:Content>