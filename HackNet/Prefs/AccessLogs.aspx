<%@ Page Title="" Language="C#" MasterPageFile="~/Prefs.master" AutoEventWireup="true" CodeBehind="AccessLogs.aspx.cs" Inherits="HackNet.Prefs.AccessLogs" %>
<asp:Content ID="AccessLogs" ContentPlaceHolderID="PrefsContent" runat="server">
	<ol class="breadcrumb" style="margin-bottom: 5px;">
		<li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
		<li>Administration</li>
		<li>Audit Logs</li>
		<li class="active"><%: Page.Title %></li>
	</ol>
	<div class="panel panel-default">
		<div class="panel-body">
			<div class="col-md-3">
				Start Date / Time
				<asp:TextBox CssClass="form-control" runat="server" ID="StartDT" TextMode="DateTime" />
			</div>
			<div class="col-md-3">
				End Date / Time
				<asp:TextBox CssClass="form-control" runat="server" ID="EndDT" extMode="DateTime"/>				
			</div>	
			<div class="col-md-3">
				<br />
				<asp:Button CssClass="btn btn-success" Text="Submit" runat="server" OnClick="SubmitBtn_Click"/>			
			</div>
			<asp:Label ID="Msg" runat="server" ForeColor="Red" />
		</div>
	</div>
	<asp:GridView runat="server" ID="ResultGrid" CssClass="table logTable" AllowPaging="true" OnPageIndexChanging="ResultGrid_PageIndexChanging" />
</asp:Content>
