<%@ Page Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="HackNet.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div class="jumbotron">
		<h1><%= Page.Title %></h1>
		<p>Unfortunately, an error within HackNet has occurred<span class="blinking">_</span></p>
	</div>
	
	<div runat="server" id="ErrorInfo" class="panel panel-default">
		<div class="panel-heading">
			Error Information
		</div>
		<div class="panel-body">
			<asp:Label ID="ErrorDescription" runat="server" />
			<asp:HyperLink NavigateUrl="~/Contact.aspx" Text="Feel free to contact us!"/>
		</div>
	</div>

</asp:Content>
