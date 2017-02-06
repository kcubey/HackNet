<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="HackNet.Payment.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    
    <link rel="stylesheet" href="/payment/backend/paymentCSS.css" />
    <script src="/Payment/backend/JSv2.js" lang="javascript" type="text/javascript"></script>
<!--    <script src="https://js.braintreegateway.com/js/braintree-2.30.0.min.js"></script>-->

    <script>
        function showConfirmPayModal() {
            $('#ConfirmPayModal').modal('show');
        }
    </script>

    <!-- =============== START MODAL CONTENT ============== -->
    <div id="ConfirmPayModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="*WARNING*" ForeColor="Red" Font-Size="Larger" Font-Bold="True"></asp:Label>
                </div>
                <div class="modal-body" style="color: black;">
                    <asp:Label ID="checkoutLbl" runat="server" Text="Checkout: " ForeColor="Black" Font-Bold="True"></asp:Label>
                    <br />
                    <asp:Label ID="modalPackageDetails" runat="server" Text="" ForeColor="Blue" Font-Bold="True"></asp:Label>
                    <br /><br />
                    <asp:Label ID="messageLbl" runat="server" Text="This purchase requires real money. Are you sure you wish to proceed?"></asp:Label>
                </div>
                <div class="modal-footer">
                    <asp:Button id="convertFromModal" CssClass="btn btn-default" runat="server" OnClick="checkoutClick" Text="Confirm" />
                    <asp:Button id="cancelPayment" CssClass="btn btn-default" runat="server" OnClick="CancelClick" Text="Cancel" />
                </div>
            </div>
        </div>
    </div>
<!-- =============== END MODAL CONTENT ============== -->

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
<!--            <asp:Button ID="checkoutBtn" type='submit' runat="server" Text="Checkout" OnClick="checkoutClick" CssClass="btn btn-success" />-->
            <asp:Button ID="confirmBtn" type='submit' runat="server" Text="Confirm" OnClick="confirmBtn_Click" CssClass="btn btn-success" />
            <asp:Button ID="CancelButton" Text="Cancel" CssClass="btn btn-success"	OnClick="CancelClick" runat="server" />

            <script>
                braintree.setup("<%=clientToken%>", "dropin", {
                    container: 'payment-form',
                    form: 'checkout-form'
                });
            </script>
        </div>
    </div>
</asp:Content>