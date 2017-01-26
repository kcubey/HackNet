<%@ Page Title="Confirm Email" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConfirmEmail.aspx.cs" Inherits="HackNet.Auth.ConfirmEmail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
		<div class="jumbotron">
		<h1><%= Page.Title %></h1>
		<p>Confirm your E-Mail to gain full access!<span class="blinking">_</span></p>
	</div>
    	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Auth/signin") %>">Authentication</a></li>
		<li><a href="<%= ResolveUrl("~/Auth/signup") %>">Registration</a></li>
		<li class="active">Email Confirmation</li>
	</ol>
   	<br />
	<div class="panel panel-default">
		<div class="panel-heading">
			Confirm Email
		</div>
		<div class="panel-body">
            <asp:Label ID="Msg" ForeColor="Red" runat="server"/>
			<table runat="server" id="ConfirmTable" class="formTable">
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
				<tr>
					<td><strong>Code:</strong></td>
					<td>
						<asp:TextBox ID="EmailCode" CssClass="pwdfield form-control" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="EmailCodeValidator"
							ControlToValidate="EmailCode"
							ErrorMessage="*"
							runat="server" />
					</td>
				</tr>
			</table>
			<asp:Button ID="EmailConfirmBtn"
				Text="Confirm Email"
				CssClass="btn btn-primary loginBtn"
				OnClick="EmailConfirm_Click"
				CausesValidation="true"
				runat="server" />
		</div>
	</div>
</asp:Content>
