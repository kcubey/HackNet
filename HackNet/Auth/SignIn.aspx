<%@ Page Title="Sign In" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignIn.aspx.cs" Inherits="HackNet.Auth.SignIn" %>

<asp:Content ID="SignInContent" ContentPlaceHolderID="MainContent" runat="server">
	<div class="jumbotron">
		<h1>Sign In</h1>
		<p>Sign in or register to access our full range of features<span class="blinking">_</span></p>
	</div>

	<div class="panel panel-default">
		<div class="panel-heading">
			Sign In
		</div>
		<div class="panel-body">
			<table runat="server" class="loginTable">
				<tr>
					<td><strong>Username:</strong></td>
					<td>
						<asp:TextBox ID="UserName" runat="server" CssClass="form-control" /></td>
					<td>
						<asp:RequiredFieldValidator
							ID="UserNameValidator"
							ControlToValidate="UserName"
							Display="Dynamic"
							ErrorMessage="*"
							ValidationGroup="valGroup1"
							runat="server" />
					</td>
				</tr>
				<tr>
					<td><strong>Password:</strong></td>
					<td>
						<asp:TextBox ID="UserPass" CssClass="pwdfield form-control" TextMode="Password" runat="server" />
						<a href="lostpw.aspx" runat="server">Forgot your password?</a>
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="UserPassValidator"
							ControlToValidate="UserPass"
							ErrorMessage="*"
							runat="server" />
					</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<asp:CheckBox ID="Persist" runat="server" />
						<strong>Remember Login</strong>
					</td>
				</tr>
				<tr>
					<td><strong>I am a...</strong></td>
					<td>
						<div class="radio">
							<asp:RadioButtonList runat="server" RepeatLayout="flow">
								<asp:ListItem Text=" Returning User" Value="false" Selected="True" />
								<asp:ListItem Text=" New User" Value="true" />
							</asp:RadioButtonList>
						</div>
					</td>
				</tr>
			</table>
			<asp:Button ID="LoginButton"
				Text="Sign In"
				CssClass="btn btn-primary loginBtn"
				OnClick="LoginClick"
				runat="server" />
			<asp:Button ID="RegisterButton"
				Text="Sign Up"
				ValidationGroup="valGroup1"
				CssClass="btn btn-success regBtn"
				OnClick="RegisterClick"
				runat="server" />
		</div>
	</div>
</asp:Content>
