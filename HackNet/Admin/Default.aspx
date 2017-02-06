<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HackNet.Admin.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
    <ol class="breadcrumb" style="margin-bottom: 5px;">
        <li><a href="<%= ResolveUrl("~/Default") %>">HackNet</a></li>
        <li>Administration</li>
        <li class="active"><%: Page.Title %></li>
    </ol>

    <div class="panel panel-default">
        <div class="jumbotron" style="padding:3%;">
            <h2>Welcome Back, Admin</h2>
            <p>Only administrators are allowed to access parts of our administration.</p>
        </div>
        <div class="panel-body" style="color: black; background-color: black;">
            <fieldset>
                <legend style="color:white;">General Statistic</legend>
                <div class="form-group row">
                    <asp:Label runat="server" Text="Total Users: " CssClass="col-xs-3 col-form-label" ForeColor="White" />
                    <asp:Label runat="server" ID="TotalNumUserLbl" CssClass="col-xs-3 col-form-label" ForeColor="White" />
                </div>
                <div class="form-group row">
                    <asp:Label runat="server" Text="Top Player " CssClass="col-xs-3 col-form-label" ForeColor="White" />
                    <asp:Label runat="server" ID="TopPlayerLbl" CssClass="col-xs-3 col-form-label" ForeColor="White" />
                </div>
            </fieldset>
            <fieldset>
                <legend style="color:white;">Mission Statistic</legend>
                <div class="form-group row">
                    <asp:Label runat="server" Text="Total Number of Mission: " CssClass="col-xs-3 col-form-label" ForeColor="White" />
                    <asp:Label runat="server" ID="TotalNumMissionLbl" CssClass="col-xs-3 col-form-label" ForeColor="White" />
                </div>
                <div class="form-group row">
                    <asp:Label runat="server" Text="Total times mission was played " CssClass="col-xs-3 col-form-label" ForeColor="White" />
                    <asp:Label runat="server" ID="TotalNumPlayedLbl" CssClass="col-xs-3 col-form-label" ForeColor="White" />
                </div>
            </fieldset>
            <fieldset>
                <legend style="color:white;">Item Statistic</legend>
                <div class="form-group row">
                    <asp:Label runat="server" Text="Total Number of Item: " CssClass="col-xs-3 col-form-label" ForeColor="White" />
                    <asp:Label runat="server" ID="TotalItemLbl" CssClass="col-xs-3 col-form-label" ForeColor="White" />
                </div>
              
            </fieldset>
        </div>
    </div>
	
</asp:Content>
