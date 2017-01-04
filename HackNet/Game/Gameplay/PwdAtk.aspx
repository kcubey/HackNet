<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PwdAtk.aspx.cs" Inherits="HackNet.Game.Gameplay.PwdAtk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row" style="border: 1px solid black; margin-left: 0; margin-right: 0;">
        <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px solid black;  padding: 0;">
            <div class="panel-header" style="background-color: slategray; margin: 0; padding: 5px;">
                <h3 style="margin: 0;">
                    <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                    hydra
                </h3>
            </div>
            <div class="panel-body" style="background-color: #f5f5f5; color: black; min-height:300px;">
                <label style="width: 20%;" class="col-xs-3 col-form-label">Target IP: </label>
                <asp:TextBox ID="TargetIPLbl" runat="server" Style="width: 150px;"></asp:TextBox>

                <fieldset style="margin-top: 2%;">
                    <legend style="font-size: 15px;">Connection Options</legend>
                    <label style="width: 20%;" class="col-xs-3 col-form-label">Port: </label>
                    <asp:TextBox ID="TargetPortLbl" runat="server" Text="22" Enabled="false" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                    <label style="width: 20%;" class="col-xs-3 col-form-label">Protocol: </label>
                    <asp:TextBox runat="server" Text="SSH" Enabled="false" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                </fieldset>
                <fieldset style="margin-top: 2%;">
                    <legend style="font-size: 15px;">Advanced Options</legend>
                    <label style="width: 20%;" class="col-xs-3 col-form-label">Target User: </label>
                    <asp:TextBox ID="TargetTxtBox" runat="server" CssClass="col-xs-3 col-form-label"></asp:TextBox>
                    <label class="col-xs-3 col-form-label">Method of Attack: </label>
                    <asp:DropDownList ID="TargetAtkTypeList" runat="server">
                        <asp:ListItem>Dictionary Attack</asp:ListItem>
                        <asp:ListItem>Brute Force Attack</asp:ListItem>
                    </asp:DropDownList>
                </fieldset>
                <br />
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
            <div class="panel-body" style="background-color: #f5f5f5; min-height:300px;">
                <h4 style="color: black;">Steps for Password Attack</h4>
                <ol class="list-group" style="color: black;">
                    <li class="list-group-item">1. Configure hydra</li>
                    <li class="list-group-item">2. Run hydra</li>
                    <li class="list-group-item">3. Gain Root Access</li>
                    <li class="list-group-item">4. Steal files</li>
                    <li class="list-group-item">5. Wipe all traces</li>
                </ol>
            </div>
        </div>
    </div>

    <div class="row" >
        <div class="col-md-9">
            <div class="panel panel-default">
                <div class="panel-header" style="background-color: grey;">
                    <h4 style="text-align: center; margin-bottom: 0;">
                        @HackNetHost:~
                    </h4>
                </div>
                <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 350px; height: 250px;">
                    <asp:Panel ID="LogPanel" runat="server"></asp:Panel>
                </div>
                <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white;">
                    <asp:Label runat="server" Text="@HackNet:~#" Width="20%"></asp:Label>
                    <asp:TextBox runat="server" ID="CmdTextBox" BackColor="#091012" BorderStyle="None" Style="min-width: 69%; width: 69%; padding: 5px;"></asp:TextBox>
                    <asp:Button runat="server" OnClick="SubCmdBtn_Click" ID="SubCmdBtn" Text="Submit" CssClass="btn btn-default" Width="10%" style="float:right;" />
                    <asp:Label runat="server" ID="CmdError"></asp:Label>
                </div>
                
            </div>
            <div class="panel panel-default">
                <div class="panel-header" style="background-color: grey;">
                    <h4 style="text-align: center; margin-bottom: 0;">
                         Nautilus
                    </h4>
                </div>
                <div class="panel-body">
                    <asp:Button ID="NautilusBtn" runat="server" CssClass="btn btn-default" Text="Steal" Enabled="false" OnCommand="NautilusBtn_Command"/>                   
                     <asp:DataList ID="NautilusView" runat="server" RepeatLayout="Table" >
                        <HeaderTemplate>
                            <asp:Table runat="server" CssClass="table">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell Width="70px">
                                        
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="500px">
                                        Name
                                    </asp:TableHeaderCell>
                                    <asp:TableHeaderCell Width="200px">
                                        Last Modified
                                    </asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Table runat="server" CssClass="table" ID="NautilusFileTable">
                                <asp:TableRow>
                                    <asp:TableCell Width="70px">                                     
                                        <asp:CheckBox runat="server" ID="NautilusCheck"/>
                                    </asp:TableCell>
                                    <asp:TableCell Width="500px">
                                        <asp:Label runat="server" Text='<%#Eval("Fname") %>'></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="200px">
                                        <asp:Label runat="server" Text='<%#Eval("LMD") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="panel panel-default" style="height:390px;">
                <div class="panel-header" style="background-color: grey;">
                    <h4 style="text-align: center; margin-bottom: 0;">
                        Hacked Password List
                    </h4>
                </div>
                <div class="panel-body" style="background-color:black;">
                     <asp:DataList ID="PwdListView" runat="server">
                         <HeaderTemplate>
                             <h4>Possible Password List</h4>
                         </HeaderTemplate>
                    <ItemTemplate>
                        <li class="list-group-item" style="background: #666666; width: 220px; margin:0 auto;">
                            <asp:Label runat="server" Text='<%#Eval("Posspwd") %>'></asp:Label>
                        </li>
                    </ItemTemplate>
                </asp:DataList>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
