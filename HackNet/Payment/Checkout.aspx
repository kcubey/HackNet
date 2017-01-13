<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="HackNet.Payment.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    
    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Checkout</h3>
		</div>
		<div class="panel-body">
            <h4>Your purchase has been confirmed!</h4>
            <hr />
            You have purchased <asp:Label ID="packageDetailsLbl" runat="server" forecolor="Red"></asp:Label>

            <br />
            Your transaction ID is <asp:Label ID="transactionId" runat="server"></asp:Label>

            <br /><br />
            An email has been sent to you regarding your purchase.
            <br />Thank you!
        </div>
    </div>
       
</asp:Content>
