﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="HackNet.Payment.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    
    <!--
        <!-- JSv2
        <!--<script src="https://js.braintreegateway.com/js/braintree-2.30.0.min.js"></script>-->

        <!-- JS v3
        <!-- Load the Client component. 
        <!--<script src="https://js.braintreegateway.com/web/3.6.0/js/client.min.js"></script>
        <script src="/Payment/backend/JSv3Client.js" lang="javascript" type="text/javascript"></script>

        <!-- Load the Hosted Fields component. 
        <!--<script src="https://js.braintreegateway.com/web/3.6.0/js/hosted-fields.min.js"></script>
        <script src="/Payment/backend/JSv3HostedFields.js" lang="javascript" type="text/javascript"></script>
    -->
        <!-- all-->
        
    <link rel="stylesheet" href="/payment/backend/paymentCSS.css" />
    <script src="/Payment/backend/JSv2.js" lang="javascript" type="text/javascript"></script>


    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Payment</h3>
		</div>
		<div class="panel-body">
            <asp:Label ID="warning" runat="server" Text="* WARNING *" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
            <p>You have chosen to use REAL currency to pay for premium items.
                <br />
                If you have NOT chosen to do so, <asp:LinkButton ID="backButton" runat="server" postbackurl="~/Game/Market.aspx">click to go back</asp:LinkButton>
            </p>

            <h3>Payment Details</h3>
            <asp:Label ID="packageDetailsLbl" runat="server" forecolor="Red"></asp:Label>
   
            <hr /><p>For Paypal, click on the Paypal button. For Credit Card, enter a valid card number and expiration date.</p>
    
            <div class="dropinBox">
                <div id="payment-form"></div>
                <input type="hidden" name="payment_method_nonce" id="payment_method_nonce"/>
            </div>

            <br />
            <asp:Button ID="checkoutBtn" type='submit' runat="server" Text="Checkout" OnClick="checkoutClick" CssClass="btn btn-success" />
            <asp:Button ID="Button1" Text="test" CssClass="btn btn-success"	runat="server" onclick="testClick"/>
            <asp:Button ID="CancelButton" Text="Cancel" CssClass="btn btn-success"	OnClick="CancelClick" runat="server" />

            <script>
                var button = document.querySelector("#checkoutBtn");
                braintree.setup("<%=clientToken%>", "dropin", {
                container: 'payment-form',
                form: 'checkout-form',
                onPaymentMethodReceived: function(obj){
                    button.addEventListener('click', function (event) {
                    var payment_method_nonce = document.getElementById("payment_method_nonce");
                    payment_method_nonce.value = "fake-valid-nonce";
                    form.submit();
                        }
                   }
                });
            </script>
        </div>
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

</asp:Content>