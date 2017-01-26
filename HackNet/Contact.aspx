<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HackNet.Contact" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .emaila, .emaila:hover, .emaila:focus, .emaila:visited{
            color:black;
        }

    </style>
    <h2><%: Title %>.</h2>
    <address>
        One Microsoft Way<br />
        Redmond, WA 98052-6399<br />
        <abbr title="Phone">P:</abbr>
        425.555.0100
    </address>

    <address>
        <strong>Support:</strong>   <a class="emaila" href="mailto:Support@example.com">Support@example.com</a><br />
        <strong>Marketing:</strong> <a class="emaila" href="mailto:Marketing@example.com">Marketing@example.com</a>
    </address>

    <h3>Get in touch with us</h3>
    <span title="subject"> Subject </span>
    <asp:TextBox ID="Subjecttxt" runat="server" CssClass="form-control" placeholder="Your subject" style="Width:100%; border-width:3px;"/> 
    <span title="content"> Text </span>
    <asp:TextBox ID="contenttxt" runat="server" CssClass="form-control" placeholder="Your text" style="Width:100%; border-width:3px;"/> 
    <span title="useremail"> Your Email </span>
    <asp:TextBox ID="useremail" runat="server" CssClass="form-control" type="email" placeholder="Your email" style="Width:100%; border-width:3px;"/> 
    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClick="btnSubmit_Click" style="margin-top:10px;" />
</asp:Content>
