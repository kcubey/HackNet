<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="PartsInfo.aspx.cs" Inherits="HackNet.Game.PartsInfo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
    <asp:Image runat="server" ID="ItemImageLoaded" />
    <br />
    <asp:Label runat="server" ID="ItemTypeLbl"></asp:Label>
    <br />
    <asp:Label runat="server" ID="ItemName"></asp:Label>
    <br />
    <asp:Label runat="server" ID="ItemPrice">SGD</asp:Label>
    <br />
    <asp:Label runat="server" ID="ItemDesc"></asp:Label>

</asp:Content>
