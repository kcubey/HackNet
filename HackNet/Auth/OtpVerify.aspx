<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OtpVerify.aspx.cs" Inherits="HackNet.Auth.OtpVerify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="jumbotron">
		<h1><%:Title %></h1>
		<p>Please enter your One Time Password to continue.</p>
	</div>
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Auth/signin") %>">Authentication</a></li>
		<li><a href="<%= ResolveUrl("~/Auth/signin") %>">Login</a></li>
		<li class="active">One Time Password</li>
	</ol>
	<br />

	<div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">One Time Password</h3>
		</div>
		<div class="panel-body">
			<p>
				Enter your One-Time Password generated from your previously set-up authenticator app.
			<br />
				(Google Authenticator, Authy, etc..)
			</p>
			<asp:Label ID="UID" ForeColor="blue" runat="server" /><br />
			<table runat="server" class="loginTable">
				<tr>
					<td><strong>OTP:</strong></td>
					<td>
						<asp:TextBox ID="OTPValue" runat="server" CssClass="form-control" ToolTip="Enter your OTP Auth Code"></asp:TextBox>
					</td>
					<td>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
							ControlToValidate="OTPValue"
							ErrorMessage="Please ensure your OTP is entered."
							ForeColor="Red">
						</asp:RequiredFieldValidator>
					</td>
				</tr>
			</table>
			<asp:Label ID="Msg" ForeColor="red" runat="server" />
			<br />
			<br />
			<asp:Button ID="OTPLogin" runat="server" Text="Login" CssClass="otpLogin btn btn-primary" OnClick="ConfirmOTP" />
			<asp:Button ID="OTPCancel" runat="server" Text="Cancel" CssClass="otpCancel btn btn-danger" OnClick="CancelOTP" CausesValidation="false" />
		</div>
	</div>
</asp:Content>
