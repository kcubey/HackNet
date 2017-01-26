<%@ Page Title="Profile Settings" Language="C#" MasterPageFile="~/Prefs.master" AutoEventWireup="true" CodeBehind="ProfileSettings.aspx.cs" Inherits="HackNet.Prefs.ProfileSettings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="PrefsContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>User Preferences</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
	<br />
	<div class="panel panel-primary">
		<div class="panel-heading">
			<h3 class="panel-title">Account Preferences</h3>
		</div>
		<div class="panel-body">
			<h4>User Details</h4>
			<table class="formTable">
				<tr>
					<td>
						<label>Username: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="usernameTxt" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="usernameTxt" ForeColor="Red"
							ErrorMessage="Username is required." Text="*" runat="server" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
							ErrorMessage="Username can only contain alphanumerics and underscores, and at least 4 characters"
							ControlToValidate="usernameTxt" Display="None" ValidationExpression="^[a-zA-Z0-9_]{4,14}$" />
					</td>
				</tr>
				<tr>
					<td>
						<label>E-mail: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="emailTxt" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="emailTxt" ForeColor="Red"
							ErrorMessage="Email Address is required." Text="*" runat="server" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
							ErrorMessage="Last name can only contain alphabets and spaces up to 50 characters"
							ControlToValidate="emailTxt" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
					</td>
				</tr>
				<tr>
					<td>
						<label>Full Name: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="fnameTxt" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="fnameTxt" ForeColor="Red"
							ErrorMessage="Full Name is required" Text="*" runat="server" />
						<asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
							ErrorMessage="Full name can only contain alphabets and spaces up to 50 characters"
							ControlToValidate="fnameTxt" Display="None" ValidationExpression="^[a-zA-Z ]{1,50}$" />
					</td>
				</tr>
				<tr>
					<td>
						<label>Date of Birth: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="DobTxt" runat="server" />
					</td>
				</tr>
				<tr>
					<td>
						<label>Password to Confirm: </label>
					</td>
					<td>
						<asp:TextBox type="password" CssClass="form-control" ID="CfmPasswordTxt" runat="server" />
					</td>
				</tr>
			</table>
			<asp:Label ID="Msg" runat="server" ForeColor="Red" />
			<br />
			<asp:Button ID="ProfileChangeBtn"
				Text="Update Details"
				CssClass="btn btn-info"
				OnClick="ProfileChange_Click"
				runat="server" />
		</div>
	</div>
</asp:Content>
