<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="MissionLogs.aspx.cs" Inherits="HackNet.Game.MissionLogs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
    <div class="panel panel-default">
        <h2>Mission Logs</h2>
        <asp:GridView runat="server" ID="MissionLog" OnLoad="MissionLog_Load" BorderStyle="None" CssClass="table" ShowHeader="true">
        </asp:GridView>
    </div>
</asp:Content>
