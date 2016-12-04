<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Prefs.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HackNet.Prefs.ChangePassword" %>
<asp:Content ID="ChangePWContent" ContentPlaceHolderID="PrefsContent" runat="server">
		<div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Change Password</h3>
		</div>
		<div class="panel-body">

			<table runat="server" class="loginTable">
				<tr>
					<td><strong>Username:</strong></td>
					<td>
						<strong><asp:Label ID="LoggedInUsername" runat="server" /></strong></td>
					<td>
					</td>
				</tr>
				<tr>
					<td><strong>Old Password:</strong></td>
					<td>
						<asp:TextBox ID="OldUserPass" CssClass="pwdfield form-control" TextMode="Password" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="RequiredFieldValidator2"
							ControlToValidate="OldUserPass"
							ErrorMessage="Cannot be empty."
							runat="server" />
					</td>
				</tr>
				<tr>
					<td><strong>New Password:</strong></td>
					<td>
						<asp:TextBox ID="NewUserPass" CssClass="pwdfield form-control" TextMode="Password" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="RequiredFieldValidator3"
							ControlToValidate="NewUserPass"
							ErrorMessage="Cannot be empty."
							runat="server" />
					</td>
				</tr>
				<tr>
					<td><strong>Confirm New:</strong></td>
					<td>
						<asp:TextBox ID="NewUserPassCfm" CssClass="pwdfield form-control" TextMode="Password" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="RequiredFieldValidator4"
							ControlToValidate="NewUserPassCfm"
							ErrorMessage="Cannot be empty."
							runat="server" />
					</td>
				</tr>
			</table>
			<asp:Button ID="ChangePWButton"
				Text="Change Password"
				CssClass="btn btn-primary loginBtn"
				OnClick="ChangePassClick"
				runat="server" />
		</div>
	</div>


	<p>
		<asp:Label ID="Msg" ForeColor="red" runat="server" />
</p>
</asp:Content>