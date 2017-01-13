<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Retry.aspx.cs" Inherits="HackNet.Payment.Retry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameHeadPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
    <h1>Oops, something went wrong!</h1>
    Your transaction failed due to <asp:Label ID="transactionError" runat="server"></asp:Label>.
    Please try again later.
</asp:Content>
