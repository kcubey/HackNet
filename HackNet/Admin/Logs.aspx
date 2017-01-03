<%@ Page Title="Database Stored Logs" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Logs.aspx.cs" Inherits="HackNet.Admin.Logs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>Administration</li>
		<li>Audit Logs</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
	<asp:GridView runat="server" ID="ResultGrid" CssClass="table table-striped table-hover" />
</asp:Content>
