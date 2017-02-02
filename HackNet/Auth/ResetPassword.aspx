<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="HackNet.Auth.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	
	<div class="jumbotron">
		<h1><%= Page.Title %></h1>
		<p>Forgot your password? Reset it!<span class="blinking">_</span></p>
	</div>

    <ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Auth/signin") %>">Authentication</a></li>
		<li><a href="<%= ResolveUrl("~/Auth/signin") %>">Others</a></li>
		<li class="active">Password Reset</li>
	</ol>

   	<br />

	<div class="panel panel-default">
		<div class="panel-heading">
			Reset Password
		</div>
		<div class="panel-body">
            <asp:Label ID="Msg" 
				ForeColor="Orange" 
				runat="server" 
				Text="You will lose access to ALL your chats as your key depends on your old password"/>

			<br />

			<table runat="server" id="ResetTable" class="formTable">
				<tr>
					<td><strong>Email:</strong></td>
					<td>
						<asp:TextBox ID="Email" runat="server" CssClass="form-control" /></td>
					<td>
						<asp:RequiredFieldValidator
							ID="EmailValidator"
							ControlToValidate="Email"
							ErrorMessage="*"
							runat="server" />
						<asp:RegularExpressionValidator
							ID="EmailRegExValidator"
							ControlToValidate="Email"
							ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
							ErrorMessage="Invalid Email Address"
							runat="server" />
					</td>
				</tr>
			</table>
			<br />
			<asp:Button ID="ResetPasswordBtn"
				Text="Send Reset Link"
				CssClass="btn btn-primary loginBtn"
				OnClick="SendResetLink_Click"
				CausesValidation="true"
				runat="server" />
		</div>
	</div>
</asp:Content>
