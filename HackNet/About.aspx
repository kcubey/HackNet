<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="HackNet.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <h3>Your application description page..</h3>
    <p>Use this area to provide additional information.</p>

    <h3>Contact Us</h3>
    <span title="subject"> Subject </span>
    <asp:TextBox ID="Subjecttxt" runat="server" CssClass="form-control" placeholder="Your subject" style="Width:100%; border-width:3px;"/> 
    <span title="content"> Text </span>
    <asp:TextBox ID="contenttxt" runat="server" CssClass="form-control" placeholder="Your text" style="Width:100%; border-width:3px;"/> 
    <span title="useremail"> Your Email </span>
    <asp:TextBox ID="useremail" runat="server" CssClass="form-control" type="email" placeholder="Your email" style="Width:100%; border-width:3px;"/> 
    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClick="btnSubmit_Click" style="margin-top:10px;" />
</asp:Content>
