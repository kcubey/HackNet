<%@ Page Title="Database Stored Logs" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Logs.aspx.cs" Inherits="HackNet.Admin.Logs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>Administration</li>
		<li>Audit Logs</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
	<div class="panel panel-default">
		<div class="panel-body">
			<div class="col-md-3">
				Category
				<asp:DropDownList CssClass="form-control" ID="DDCategory" AutoPostBack="true" OnSelectedIndexChanged="DDCategory_SelectedIndexChanged" runat="server">
					<asp:ListItem Value="Game" Text="Game" />
					<asp:ListItem Value="Security" Text="Security" Selected="True" />
					<asp:ListItem Value="Payment" Text="Payment" />
					<asp:ListItem Value="Profile" Text="Profile" />
					<asp:ListItem Value="Error" Text="Error" />
				</asp:DropDownList>
			</div>
			<div class="col-md-3">
				Start Date / Time
				<asp:TextBox CssClass="form-control" runat="server" ID="StartDT" type="date"/>
			</div>
			<div class="col-md-3">
				End Date / Time
				<asp:TextBox CssClass="form-control" runat="server" ID="EndDT" type="date"/>				
			</div>	
			<div class="col-md-3">
				<asp:Button CssClass="btn btn-success" Text="Submit" runat="server" OnClick="SubmitBtn_Click"/>			
			</div>
			<asp:Label ID="Msg" runat="server" ForeColor="Red" />
		</div>
	</div>
	<asp:GridView runat="server" ID="ResultGrid" CssClass="table logTable" AllowPaging="true" OnPageIndexChanging="ResultGrid_PageIndexChanging" />
</asp:Content>
