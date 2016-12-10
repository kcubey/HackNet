<%@ Page Title="Registration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="HackNet.Auth.SignUp" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceholder" runat="server">
			<script src='https://www.google.com/recaptcha/api.js'></script>
</asp:Content>
<asp:Content ID="SignUpContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1><%: Title %></h1>
        <p>Welcome to HackNet, please login or register to access our features.</p>
    </div>
    <ol class="breadcrumb" style="margin-bottom: 5px;">
        <li><a href="<%= ResolveUrl("~/Auth/signin") %>">Authentication</a></li>
        <li><a href="<%= ResolveUrl("~/Auth/signup") %>">Registration</a></li>
        <li class="active">Details</li>
    </ol>
    <br />
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Registration Form</h3>
        </div>
        <div class="panel-body">
            <div class="alert alert-info" role="alert" runat="server" visible="false" id="ConfirmSent">
                Please verify your email address!
			<br />
                An email has been sent to your email at
				<asp:Label runat="server" ID="EmailAddrSent" />
                <br />
                Verification email will only be valid for 24 hours
            </div>
            <div runat="server" id="RegFields">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="DarkRed"
                    DisplayMode="BulletList" ShowSummary="true" HeaderText="Errors:" />
                <asp:Label ID="Msg" ForeColor="red" runat="server" />
                <table runat="server" class="regTable">

                    <tr>
                        <td><strong>E-mail Address:</strong></td>
                        <td>
                            <asp:TextBox ID="UserEmail" CssClass="form-control" runat="server" placeholder="email@address.com" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="UserEmail" ForeColor="Red"
                                ErrorMessage="Email Address is required." Text="*" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                ErrorMessage="Last name can only contain alphabets and spaces up to 50 characters"
                                ControlToValidate="UserEmail" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Confirm E-Mail:</strong></td>
                        <td>
                            <asp:TextBox ID="UserEmailCfm" CssClass="form-control" runat="server" placeholder="email@address.com" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="UserEmailCfm" ForeColor="Red"
                                ErrorMessage="Email Address confirmation required." Text="*" runat="server" />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="UserEmailCfm"
                                ControlToCompare="UserEmail" Display="None" ErrorMessage="Emails entered do not match!" />
                        </td>
                    </tr>
                    <tr>
                          <td></td>
                    </tr>
                    <tr>
                        <td><strong>Username: </strong></td>
                        <td>
                            <asp:TextBox ID="UserName" runat="server" CssClass="form-control" /></td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="UserName" ForeColor="Red"
                                ErrorMessage="Username is required." Text="*" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                ErrorMessage="Username can only contain alphanumerics and underscores, and at least 4 characters"
                                ControlToValidate="UserName" Display="None" ValidationExpression="^[a-zA-Z_]{4,40}$" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Password:</strong></td>
                        <td>
                            <asp:TextBox ID="UserPass" CssClass="pwdfield form-control" TextMode="Password" runat="server" placeholder="" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="UserPass" ForeColor="Red"
                                ErrorMessage="Password is required." Text="*" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                ErrorMessage="Password requires an uppercase, lowercase and a number."
                                ControlToValidate="UserPass" Display="None" ValidationExpression="^[a-zA-Z0-9_]{8,50}$" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Confirm Password:</strong></td>
                        <td>
                            <asp:TextBox ID="UserPassCfm" CssClass="pwdfield form-control" TextMode="Password" runat="server" placeholder="" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="UserPassCfm" ForeColor="Red"
                                ErrorMessage="Password confirmation required" Text="*" runat="server" />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="UserPassCfm"
                                ControlToCompare="UserPass" Display="None" ErrorMessage="Passwords entered do not match!" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Full Name:</strong></td>
                        <td>
                            <asp:TextBox ID="FullName" CssClass="form-control" runat="server" />
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="FullName" ForeColor="Red"
                                ErrorMessage="Full Name is required" Text="*" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                ErrorMessage="Full name can only contain alphabets and spaces up to 50 characters"
                                ControlToValidate="FullName" Display="None" ValidationExpression="^[a-zA-Z ]{1,50}$" />
                        </td>
                    </tr>
                    <tr>
                        <td><strong>Captcha:</strong></td>
                        <td>
                            <div class="g-recaptcha" data-sitekey="6LdUsQsUAAAAAHcvokhKFSxCtVsYGSI5MO5ZI2Ke"></div>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:Button ID="RegisterButton" Text="Sign Up" CssClass="btn btn-success" OnClick="RegisterClick" runat="server" />
            </div>
        </div>
    </div>
</asp:Content>
