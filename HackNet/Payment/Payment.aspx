<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="HackNet.Payment.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/payment/backend/paymentCSS.css" />


    <!-- JSv2-->
    <script src="https://js.braintreegateway.com/js/braintree-2.30.0.min.js"></script>

    <!-- JS v3-->
    <!-- Load the Client component. -->
    <script src="https://js.braintreegateway.com/web/3.6.0/js/client.min.js"></script>
    <!-- Load the Hosted Fields component. -->
    <script src="https://js.braintreegateway.com/web/3.6.0/js/hosted-fields.min.js"></script>

    <!-- all-->
    <script src="/Payment/backend/paymentJS.js" lang="javascript" type="text/javascript"></script>


    <h1>Welcome!</h1>
    <p>You have chosen to use REAL currency to pay for premium items.
        <br />
        If you have NOT chosen to do so, <a href="#">click to go back</a>.
    </p>
    <h3>Payment Details</h3>
        <asp:DropDownList ID="PackageList" runat="server" ForeColor="Black" 
            AutoPostBack="true" onselectedindexchanged="pkgConfirm_indexChange" ClientIDMode="Static">
            <asp:ListItem Text="Package A - SGD$10" Value="0"></asp:ListItem>
            <asp:ListItem Text="Package B - SGD$20" Value="1"></asp:ListItem>
            <asp:ListItem Text="Package C - SGD$30" Value="2"></asp:ListItem>
            <asp:ListItem Text="Package D - SGD$40" Value="3"></asp:ListItem>
        </asp:DropDownList>
    <asp:Label ID="pkgConfirm" runat="server" ForeColor="Black" ClientIDMode="Static"></asp:Label>
    
    <h3>JS v2</h3>
    <div id="payment-form"></div>
    <input type="submit" value="Pay $10" />

            <div class="input-wrapper amount-wrapper">
                <input id="amount" name="amount" type="tel" min="1" placeholder="Amount" value="10">
            </div>            

            <button class="button" type="submit"><span>Test Transaction</span></button>

    <br /><br />

    <h3>JS v3</h3>

  <div id="error-message"></div>

  <br /><label for="card-number">Card Number: </label>
  <div class="hosted-field" id="card-number"></div>

  <br /><label for="cvv">CVV: </label>
  <div class="hosted-field" id="cvv"></div>

  <br /><label for="expiration-date">Expiration Date:</label>
  <div class="hosted-field" id="expiration-date"></div>

    <br />
  <input type="hidden" name="payment-method-nonce">
  <input type="submit" value="Pay $10" disabled>


</asp:Content>