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
			<table class="loginTable">
				<tr>
					<td>
						<label>Username: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="usernameTxt" runat="server" Font-Size="Medium"></asp:TextBox></td>
				</tr>
				<tr>
					<td>
						<label>E-mail: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="emailTxt" runat="server" Font-Size="Medium"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>First Name: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="FNameTxt" runat="server" Font-Size="Medium"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>Last Name: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="LNameTxt" runat="server" Font-Size="Medium"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<label>Date of Birth: </label>
					</td>
					<td>
						<asp:TextBox CssClass="form-control" ID="DobTxt" runat="server" Font-Size="Medium"></asp:TextBox>
					</td>
				</tr>
			</table>
			<h4>Contact Preferences</h4>
			<table>
				<tr>
					<td>
						<input type="checkbox" class="form-control" /></td>
					<td>Receive marketing information about HackNet</td>
				</tr>
			</table>
			<asp:Label ID="Msg" runat="server" />
			<asp:Button ID="ProfileChangeBtn"
				Text="Update Details"
				CssClass="btn btn-info"
				OnClick="ProfileChange_Click"
				runat="server" />
		</div>
	</div>
</asp:Content>
