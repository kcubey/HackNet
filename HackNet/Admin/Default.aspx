<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HackNet.Admin.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>Administration</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
</asp:Content>
