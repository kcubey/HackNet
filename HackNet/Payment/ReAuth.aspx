<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="ReAuth.aspx.cs" Inherits="HackNet.Payment.ReAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameHeadPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
        <h1>You have chosen to buy
            <asp:Label ID="pkgName" runat="server" Text="Package A" 
                ForeColor="Blue" CssClass="btn btn-success" Font-Bold="True" Font-Size="Large" BackColor="Black"></asp:Label>
        </h1> 
    <h5>Please enter your details below for re-authentication</h5>
    <hr />
    <h4>Email: </h4>    <asp:TextBox ID="TextBox1" runat="server" ForeColor="Black"></asp:TextBox>
    <br /><br />
    <h4>Password: </h4>    <asp:TextBox ID="TextBox2" runat="server" ForeColor="Black"></asp:TextBox>
    <br /><br />
    <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Authenticate" Font-Size="Large" PostBackUrl="~/Payment/payment.aspx"></asp:LinkButton>
    <br /><br />
    <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Back" Font-Size="Large" PostBackUrl="~/Payment/market.aspx"></asp:LinkButton>


</asp:Content>
