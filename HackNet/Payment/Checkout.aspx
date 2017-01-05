<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="HackNet.Payment.Checkout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    

    <h1>Your payment has been confirmed!</h1>
    You have purchased <asp:Label ID="packageBought" runat="server" Text="Package A"></asp:Label> at 
    <asp:Label ID="packagePrice" runat="server" Text="SGD$10"></asp:Label>.

    <br /><br />
    An email has been sent to you confirming your purchase. Thank you!

</asp:Content>
