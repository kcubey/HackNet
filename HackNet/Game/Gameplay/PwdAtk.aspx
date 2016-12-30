<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PwdAtk.aspx.cs" Inherits="HackNet.Game.Gameplay.PwdAtk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
        <div class="row" style="border:1px solid black; height:200px; margin-left:0; margin-right:0;">
            <div class="col-xs-12 col-sm-6 col-md-8" style="border:1px solid black; height:200px; padding:0; ">
                <div class="panel-header" style="background-color:slategray; margin:0; padding:5px;">
                    <h4 style="margin:0;">Test</h4>
                </div>
                <div class="panel-body">

                </div>
            </div>
            <div class="col-xs-6 col-md-4" style="border:1px solid black; height:200px; ">

            </div>


        </div>

        
   
    <div class="panel panel-default" data-step="5" data-intro="This is your terminal" data-position='right'>
        <div class="panel-header" style="background-color: grey;">
            <h4 style="text-align: center; margin-bottom: 0;">
                <asp:Label runat="server" Text="user"></asp:Label>@HackNetHost:~
            </h4>
        </div>
        <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 400px; height: 250px;">
            <asp:Panel ID="LogPanel" runat="server"></asp:Panel>
        </div>
        <div class="panel-footer" data-step="6" data-intro="You enter your command here." data-position='right' style="background-color: #091012; border-top: 1px solid white; padding: ;">
            <asp:Label runat="server" Text="username@HackNet:~#"></asp:Label>
            <asp:TextBox runat="server" ID="AtkTextBx" BackColor="#091012" BorderStyle="None" Style="min-width: 50%; width: 80%; padding: 5px;"></asp:TextBox>
        </div>

    </div>
    
    
</asp:Content>
