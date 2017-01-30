<%@ Page Title="Local Logs" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="LocalLogs.aspx.cs" Inherits="HackNet.Admin.LocalLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
	<div class="panel panel-default">
		<div class="panel-heading">
			Locally-stored Logs
		</div>
		<div class="panel-body">
			<asp:Label ID="LogsLabel" runat="server" />
		</div>
	</div>
</asp:Content>
