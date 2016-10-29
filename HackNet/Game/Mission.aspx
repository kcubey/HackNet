<%@ Page Title="Mission" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Mission.aspx.cs" Inherits="HackNet.Game.Mission" %>

<asp:Content ID="MissionPageContent" ContentPlaceHolderID="GameContent" runat="server">

    <div class="panel panel-default">
        <div class="panel panel-body">
            <div class="col-sm-12 col-md-9" style="background-image: url(../Content/Images/mission.png); background-size: cover; height: 300px;">
                <button type="button" class="btn btn-default">Attack</button>



            </div>
            <div class="col-sm-12 col-md-3" style="border: 1px solid green;">
                Machines available:

            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-header" style="background-color: grey;">
            <h4 style="text-align: center; margin-bottom:0;">
                <asp:Label runat="server" Text="user"></asp:Label>@HackNetHost:~
            </h4>
        </div>
        <div class="panel-body" style="border-radius:0; background-color:#091012; overflow-y:auto; max-height:400px; height:350px;" >
           
        </div>
        <div class="panel-footer" style="background-color:#091012; border-top:1px solid white; padding:0;">
            <asp:Label runat="server" Text="username@HackNet:~#"></asp:Label>
            <asp:TextBox runat="server" BackColor="#091012" BorderStyle="None" style="min-width:70%; padding:5px;"></asp:TextBox>
        </div>
        
    </div>
</asp:Content>
