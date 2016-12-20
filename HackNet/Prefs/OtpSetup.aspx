<%@ Page Title="Configure 2FA" Language="C#" MasterPageFile="~/Prefs.master" AutoEventWireup="true" CodeBehind="OtpSetup.aspx.cs" Inherits="HackNet.Prefs.OtpSetup" %>

<asp:Content ID="OtpSetupContent" ContentPlaceHolderID="PrefsContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>Security Preferences</li>
		<li>Authentication Configuration</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
	<br />
	<div runat="server" id="ExistingOTP" class="alert alert-success" role="alert" visible="false">
		Hey, it seems like you already have 2FA set up!
		<asp:Button ID="OTPCancelBtn" runat="server" Text="Disable 2FA" CssClass="btn btn-danger otpCancelSetup" OnClick="DisableTOTP_Button" />
	</div>
	<div runat="server" ID="OTPStep1" class="panel panel-primary">
		<div class="panel-heading">
			<h3 class="panel-title">Two-Factor Authentication Setup (Step 1)</h3>
		</div>
		<div class="panel-body">
			<div class="setupRow">
				<div class="setupRowLeft">
					<p>Please authenticate again by entering your password</p>
					<br />
					<table class="loginTable">
						<tr>
							<td><strong>Current Password:  </strong></td>
							<td>
								<asp:TextBox ID="CurrPWTxt" CssClass="otpSetupValue form-control" runat="server" TextMode="Password" /></td>
						</tr>
					</table>
					<asp:Button ID="OTPSubmitBtn" runat="server" Text="Check Password" CssClass="btn btn-success otpEnable" OnClick="VerifyPw_Button" />
					<asp:Label runat="server" ID="Msg" ForeColor="Red" /><br>
				</div>
			</div>
		</div>
	</div>
	<div runat="server" ID="OTPStep2" class="panel panel-primary" visible="false">
		<div class="panel-heading">
			<h3 class="panel-title">Two-Factor Authentication Setup (Step 2)</h3>
		</div>
		<div class="panel-body">
			<div class="setupRow">
				<div class="setupRowLeft">
					<p><strong>Use the QR code to configure your Two-Factor Authentication on your TOTP app on multiple devices</strong></p>
					<ol>
						<li>Download any OTP Generator app, E.g. Authy, Google Authenticator, etc.</li>
						<li>Open the app, then scan the QR code to the right or manually enter this code: <code>
							<asp:Label runat="server" ID="Base32Lbl" Text="TOTPSECRETHERE"></asp:Label></code></li>
						<li>Enter the current six-digit numerical passcode from the application to verify that your device is properly configured</li>
					</ol>
					<br />
					<table class="loginTable">
						<tr>
							<td><strong>Generated OTP:  </strong></td>
							<td>
								<asp:TextBox ID="GeneratedOtpTxt" CssClass="otpSetupValue form-control" runat="server" /></td>
						</tr>
					</table>
					<br />
					<asp:Label runat="server" ID="Msg2" ForeColor="Red" /><br>
					<asp:Button ID="Button1" runat="server" Text="Enable 2FA" CssClass="btn btn-success otpEnable" OnClick="VerifyOTP_Button" />
				</div>
				<div class="setupRowRight" runat="server" id="Div1">
					<asp:Image ID="Image1" runat="server" AlternateText="QR Code Not Available" Height="300px" Width="300px" />
				</div>
			</div>
		</div>
	</div>
</asp:Content>
