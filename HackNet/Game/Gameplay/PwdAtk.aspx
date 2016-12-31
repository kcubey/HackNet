<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PwdAtk.aspx.cs" Inherits="HackNet.Game.Gameplay.PwdAtk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="border: 1px solid black; margin-left: 0; margin-right: 0;">
        <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px solid black; padding: 0;">
            <div class="panel-header" style="background-color: slategray; margin: 0; padding: 5px;">
                <h3 style="margin: 0;">
                    <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                    hydra
                </h3>
            </div>
            <div class="panel-body" style="background-color: #f5f5f5; color:black;">
                <label style="width:20%;" class="col-xs-3 col-form-label">Target IP: </label>
                <asp:TextBox ID="TargetIPLbl" runat="server" style="width:150px;"></asp:TextBox>
                
                <fieldset style="margin-top:2%;">
                    <legend style="font-size:15px;">Connection Options</legend>                   
                    <label style="width:20%;" class="col-xs-3 col-form-label">Port: </label>
                    <asp:TextBox ID="TargetPortLbl" runat="server" Text="22" Enabled="false" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                    <label style="width:20%;" class="col-xs-3 col-form-label">Protocol: </label>
                    <asp:TextBox  runat="server" Text="SSH" Enabled="false" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                </fieldset>
                <fieldset style="margin-top:2%;">
                    <legend style="font-size:15px;">Advanced Options</legend>
                    <label style="width:20%;" class="col-xs-3 col-form-label">Target User: </label>
                    <asp:TextBox ID="TargetTxtBox" runat="server" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                    <label class="col-xs-3 col-form-label">Method of Attack: </label>
                    <asp:DropDownList ID="TargetAtkTypeList" runat="server">
                        <asp:ListItem>Dictionary Attack</asp:ListItem>
                        <asp:ListItem>Brute Force Attack</asp:ListItem>
                    </asp:DropDownList>
                </fieldset><br />
                <asp:Label runat="server" ID="ErrorLbl"></asp:Label>
                <br />
                <asp:Button runat="server" ID="ConfigBtn" CssClass="btn-primary" Text="Configure" OnClick="ConfigBtn_Click" />
            </div>

        </div>
        <div class="col-xs-6 col-md-4" style="border: 1px solid black; padding: 0;">
            <div class="panel-header" style="background-color: slategray; margin: 0; padding: 5px;">
                <h3 style="margin: 0;">
                    <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                    Instruction
                </h3>
            </div>
            <div class="panel-body" style="background-color: #f5f5f5; height: 100%;">
                <h4 style="color: black;">Steps for Password Attack</h4>
                <ol class="list-group" style="color:black;">
                    <li class="list-group-item">1. Configure hydra</li>
                    <li class="list-group-item">2. Run hydra</li>
                    <li class="list-group-item">3. Gain Root Access</li>
                    <li class="list-group-item">4. Steal files</li>
                    <li class="list-group-item">5. Wipe all traces</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="panel panel-default">
        <div class="panel-header" style="background-color: grey;">
            <h4 style="text-align: center; margin-bottom: 0;">
                <asp:Label runat="server" Text="user"></asp:Label>@HackNetHost:~
            </h4>
        </div>
        <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 350px; height: 250px;">
            <asp:Panel ID="LogPanel" runat="server"></asp:Panel>
        </div>
        <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white;">
            <asp:Label runat="server" Text="username@HackNet:~#"></asp:Label>
            <asp:TextBox runat="server"  ID="CmdTextBox" BackColor="#091012" BorderStyle="None" Style="min-width: 50%; width: 80%; padding: 5px;"></asp:TextBox>
        </div>
    </div>
    
    
</asp:Content>
