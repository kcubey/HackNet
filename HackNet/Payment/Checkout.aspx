<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="HackNet.Payment.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    
    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Checkout - Your purchase has been confirmed!</h3>
		</div>
		<div class="panel-body">
            You have purchased <asp:Label ID="packageDetailsLbl" runat="server" forecolor="Red"></asp:Label>
            <br />
            Your transaction ID is <asp:Label ID="transactionId" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />
            An email has been sent to you regarding your purchase, thank you!
        </div>
    </div>
       
</asp:Content>
