<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HackNet.Contact" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .emaila, .emaila:hover, .emaila:focus, .emaila:visited{
            color:black;
        }

    </style>
    <h2><%: Title %>.</h2>

    <address>
        <strong>Support:</strong>   <a class="emaila" href="mailto:support@hacknet.com">support@hacknet.com</a><br />
        <strong>Marketing:</strong> <a class="emaila" href="mailto:marketing@hacknet.com">marketing@hacknet.com</a>
    </address>

    <h3>Get in touch with us</h3>
    <span title="subject"> Subject </span>
    <asp:TextBox ID="Subjecttxt" runat="server" CssClass="form-control" placeholder="Your Subject" style="Width:100%; border-width:3px;"/> 
    <asp:RegularExpressionValidator
        Display="Dynamic"
        ControlToValidate="Subjecttxt"
        ID="RegularExpressionValidatorForSubjecttxt"
        ValidationExpression="^[a-zA-Z0-9'-'\#\,\?\*\-\(\)\._\&\+\/\s]{0,200}$"
        runat="server"
        ForeColor="Red"
        ErrorMessage="Only Special characters allowed are '*#?()/&+-_. Maximum 200 characters allowed." ValidationGroup="ItemUpdate">
    </asp:RegularExpressionValidator>
    <br />
    <span title="content"> Your Message </span>
    <asp:TextBox ID="contenttxt" runat="server" CssClass="form-control" placeholder="Your text here" TextMode="MultiLine" style="Width:100%; height:100px; border-width:3px;"/> 
    <asp:RegularExpressionValidator
        Display="Dynamic"
        ControlToValidate="contenttxt"
        ID="RegularExpressionValidatorForcontenttxt"
        ValidationExpression="^[a-zA-Z0-9'-'\#\,\?\*\-\(\)\._\&\+\/\s]{0,10000}$"
        runat="server"
        ForeColor="Red"
        ErrorMessage="Only Special characters allowed are '*#?()/&+-_. Maximum 10000 characters allowed." ValidationGroup="ItemUpdate">
    </asp:RegularExpressionValidator>
    <br />
    <span title="useremail"> Your Email </span>
    <asp:TextBox ID="useremail" runat="server" CssClass="form-control" type="email" placeholder="SoWeCanReplyToYou@youremail.com" style="Width:100%; border-width:3px;"/> 
    <asp:RegularExpressionValidator
        Display="Dynamic"
        ControlToValidate="useremail"
        ID="RegularExpressionValidatorForuseremail"
        ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
        runat="server"
        ForeColor="Red"
        ErrorMessage="Please give a valid email." ValidationGroup="ItemUpdate">
    </asp:RegularExpressionValidator>
    <br />
    <asp:Button ID="btnSubmit" runat="server" CssClass="btn" Text="Submit" OnClick="btnSubmit_Click" style="margin-top:10px;" />
    <br /><br />
    <p style="font-size:0.7em;">*This email will automatically be sent to an admin and reply to your specified email in 2-5 days.</p>
</asp:Content>
