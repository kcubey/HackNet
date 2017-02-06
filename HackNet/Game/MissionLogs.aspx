<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="MissionLogs.aspx.cs" Inherits="HackNet.Game.MissionLogs" %>

<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-header" style="padding:1%;">
            <h2>Mission Logs</h2>
        </div>

        <div class="panel-body" style="color: white; background-color: black;">
            <asp:GridView runat="server" ID="MissionLog" OnLoad="MissionLog_Load" BorderStyle="None" CssClass="table" ShowHeader="true">
            </asp:GridView>
        </div>
    </div>
</asp:Content>
