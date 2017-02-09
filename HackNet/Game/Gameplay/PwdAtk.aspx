<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ViewStateEncryptionMode="Always" CodeBehind="PwdAtk.aspx.cs" Inherits="HackNet.Game.Gameplay.PwdAtk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/Tutorial/introjs.css" rel="stylesheet">
    <script>
        function showFinishPrompt() {
            $('#missionSumModel').modal('show');
        }
        function showTutorial() {
            javascript: introJs().start();
        }
        function CancelReturnKey() {
            if (window.event.keyCode == 13)
                return false;
        }
    </script>
    <div id="missionSumModel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <asp:Label runat="server" ID="SummaryTitle" Font-Size="Larger"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:Table runat="server" ForeColor="Black" CssClass="table">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell ColumnSpan="4">Mission Summary</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <label>Mission Name: </label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:Label runat="server" ID="MisNameLbl"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <label>Mission IP Address:</label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:Label runat="server" ID="MisIPLbl"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <label>Mission Summary:</label>
                            </asp:TableCell>
                            <asp:TableCell ColumnSpan="3">
                                <asp:Label runat="server" ID="MisSumLbl"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell  ColumnSpan="4">Rewards</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <label>EXP: </label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="MisExpLbl"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <label>Coins Earned: </label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="MisCoinLbl"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>
                                <label>Item Name: </label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="ItemNameLbl"></asp:Label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <label>Item Bonus: </label>
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="ItemBonusLbl"></asp:Label>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="4">
                                <asp:Image runat="server" ID="ItemImage" Width="100px" Height="100px" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="ExitBtn" runat="server" CssClass="btn btn-default" OnClick="ExitBtn_Click" Text="Exit" />
                </div>
            </div>
        </div>
    </div>

    <div onkeypress="return CancelReturnKey();">
        <div class="row" style="border: 1px solid black; margin-left: 0; margin-right: 0;">
            <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px solid black; padding: 0;" data-step="1" data-intro="This is hydra, it is a tool used by numerous hackers around the world, what it does is, it does a brute force attack into the computer system of a company in efforts to gain access to the server." data-position='right'>
                <div class="panel-header" style="background-color: slategray; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        hydra
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; color: black; min-height: 300px;">
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
                    <asp:Button runat="server" ID="ConfigBtn" CssClass="btn-primary" Text="Configure" OnClick="ConfigBtn_Click" />
                    <br />
                    <asp:Label runat="server" ID="ErrorLbl"></asp:Label>
                    <br />
                    <asp:RegularExpressionValidator ID="IPValidator" runat="server"
                        ForeColor="Red"
                        ErrorMessage="Please enter a valid IP address"
                        ControlToValidate="TargetIPLbl"
                        ValidationExpression="([0-9]{1,3}\.|\*\.){3}([0-9]{1,3}|\*){1}" /><br />
                    <asp:RegularExpressionValidator runat="server" ID="TargetRegValidator"
                        ControlToValidate="TargetTxtBox"
                        ForeColor="Red"
                        ErrorMessage="Invalid Input"
                        ValidationExpression="^[a-zA-Z0-9_\s]*$"></asp:RegularExpressionValidator>
                </div>

            </div>
            <div class="col-xs-6 col-md-4" style="border: 1px solid black; padding: 0;">
                <div class="panel-header" style="background-color: slategray; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        Instruction
                        
            <a class="glyphicon glyphicon-question-sign" runat="server" id="HelpBtn" style="float: right; color: greenyellow; font-size: 25px; text-decoration: none;" href="javascript:void(0);" onclick="javascript:introJs().start();" data-step="5" data-intro="This is the whole game interface, if you are lost during the game, you can refer to this button again to look through the tutorial again" data-position='right'></a>
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; min-height: 300px;">
                    <ol class="list-group" style="color: black;">
                        <li class="list-group-item"><asp:Label runat="server" ID="Step1Lbl" ForeColor="Red" Text="1. Configure hydra" /></li>
                        <li class="list-group-item"><asp:Label runat="server" ID="Step2Lbl" ForeColor="Red" Text="2. Run hydra" /></li>
                        <li class="list-group-item"><asp:Label runat="server" ID="Step3Lbl" ForeColor="Red" Text="3. Enter password to gain root access" /></li>
                        <li class="list-group-item"><asp:Label runat="server" ID="Step4Lbl" ForeColor="Red" Text="4. Run nautilas" /></li>
                        <li class="list-group-item"><asp:Label runat="server" ID="Step5Lbl" ForeColor="Red" Text="5. Steal Secret file" /></li>
                    </ol>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-9">
                <div class="panel panel-default" data-step="2" data-intro="This is the command prompt which you allows you to type in commands to do specific tasks." data-position='right'>
                    <div class="panel-header" style="background-color: grey;">
                        <h4 style="text-align: center; margin-bottom: 0;">@HackNetHost:~
                        </h4>
                    </div>
                    <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 350px; height: 250px;">
                        <asp:Panel ID="LogPanel" runat="server"></asp:Panel>
                    </div>
                    <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white;">
                        <asp:Label runat="server" Text="@HackNet:~#" Width="20%"></asp:Label>
                        <asp:TextBox runat="server" ID="CmdTextBox" BackColor="#091012" BorderStyle="None" Style="min-width: 69%; width: 69%; padding: 5px;"></asp:TextBox>
                        <asp:Button runat="server" OnClick="SubCmdBtn_Click" ID="SubCmdBtn" Text="Submit" CssClass="btn btn-default" Width="10%" Style="float: right;" />
                        <asp:Label runat="server" ID="CmdError"></asp:Label>
                        <br />
                        <asp:RegularExpressionValidator runat="server" ID="CmdRegValidator"
                            ControlToValidate="CmdTextBox"
                            ForeColor="Red"
                            ErrorMessage="Invalid Input"
                            ValidationExpression="^[a-zA-Z0-9_\s-]*$"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="panel panel-default" data-step="3" data-intro="This is the nautilas, it is a graphical interface that most servers use to help navigate around files and directories." data-position='right'>
                    <div class="panel-header" style="background-color: grey;">
                        <h4 style="text-align: center; margin-bottom: 0;">Nautilus
                        </h4>
                    </div>
                    <div class="panel-body">
                        <asp:DataList ID="NautilusView" runat="server" RepeatLayout="Table">
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
                                            <asp:LinkButton runat="server" ID="StealLinkBtn" OnCommand="StealLinkBtn_Command" CommandArgument='<%#Eval("Fname") %>' Text="Steal"></asp:LinkButton>
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
                <div class="panel panel-default" style="height: 390px;" data-step="4" data-intro="This is where a list of passwords would be generated. To use this list to your advantage, type in 'su -root -P hackedPassword. To hopefully gain root access to the system'" data-position='right'>
                    <div class="panel-header" style="background-color: grey;">
                        <h4 style="text-align: center; margin-bottom: 0;">Hacked Password List
                        </h4>
                    </div>
                    <div class="panel-body" style="background-color: black;">
                        <asp:DataList ID="PwdListView" runat="server">
                            <HeaderTemplate>
                                <h4>Possible Password List</h4>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item" style="background: #666666; width: 220px; margin: 0 auto;">
                                    <asp:Label runat="server" Text='<%#Eval("Posspwd") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script type="text/javascript" src="../../Content/Tutorial/intro.js"></script>
</asp:Content>
