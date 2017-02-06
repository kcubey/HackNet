<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ViewStateEncryptionMode="Always" CodeBehind="SqlIn.aspx.cs" Inherits="HackNet.Game.Gameplay.SqlIn" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/Tutorial/introjs.css" rel="stylesheet">
    <script type="text/javascript">
        function CancelReturnKey() {
            if (window.event.keyCode == 13)
                return false;
        }
        function showTutorial() {
            javascript: introJs().start();
        }
        function showFinishPrompt() {
            $('#missionSumModel').modal('show');
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
                            <asp:TableHeaderCell>Rewards</asp:TableHeaderCell>
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
            <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px solid black; padding: 0;">
                <div class="panel-header" style="background-color: crimson; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        SQLInjector
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; color: black; min-height: 300px;">
                    <label style="width: 20%;" class="col-xs-3 col-form-label">Target IP: </label>
                    <asp:TextBox ID="TargetIPTxtBox" runat="server" Style="width: 166px;"></asp:TextBox>
                    <fieldset style="margin-top: 2%;">
                        <legend style="font-size: 15px;">Connection Options</legend>
                        <label style="width: 20%;" class="col-xs-3 col-form-label">Brwoser Type: </label>
                        <asp:DropDownList ID="TargetAtkTypeList" runat="server">
                            <asp:ListItem>FireFox</asp:ListItem>
                            <asp:ListItem>Opera</asp:ListItem>
                            <asp:ListItem>Google Chrome</asp:ListItem>
                            <asp:ListItem>Internet Explorer</asp:ListItem>
                        </asp:DropDownList>
                    </fieldset>
                    <fieldset style="margin-top: 2%;">
                        <legend style="font-size:15px;">Page Attribute</legend>
                        <label style="width: 20%;" class="col-xs-3 col-form-label">Cookie: </label>
                        <asp:TextBox runat="server" CssClass="form-control" Text="id=c60b7d91100007akt=1721892845667485|et=1095|cs=2adawdasd" Enabled="false" />
                        <br />
                        <label style="width: 20%;" class="col-xs-3 col-form-label">CharSet: </label>
                        <asp:TextBox runat="server" CssClass="form-control" Text="utf-8" Enabled="false" />
                    </fieldset>
                    <br />
                    <asp:Label runat="server" ID="ErrorLbl"></asp:Label><br />
                    <asp:RegularExpressionValidator ID="IPValidator" runat="server"
                        ForeColor="Red"
                        ErrorMessage="Please enter a valid IP address"
                        ControlToValidate="TargetIPTxtBox"
                        ValidationExpression="([0-9]{1,3}\.|\*\.){3}([0-9]{1,3}|\*){1}" /><br />
                    <asp:Button runat="server" ID="ConfigSQL" CssClass="btn-primary" Text="Configure" OnClick="ConfigSQL_Click" />

                </div>
            </div>
            <div class="col-xs-6 col-md-4" style="border: 1px solid black; padding: 0;">
                <div class="panel-header" style="background-color: crimson; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        Instruction
                         <a class="glyphicon glyphicon-question-sign" runat="server" id="HelpBtn" style="float: right; color: greenyellow; font-size: 25px; text-decoration: none;" href="javascript:void(0);" onclick="javascript:introJs().start();" data-step="5" data-intro="This is the whole game interface, if you are lost during the game, you can refer to this button again to look through the tutorial again" data-position='right'></a>
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; min-height: 300px;">
                    <h4 style="color: black;">Steps for SQL Injection Attack</h4>
                    <ol class="list-group" style="color: black;">
                        <li class="list-group-item">1. Configure SQLInjector</li>
                        <li class="list-group-item">2. Run SQLInjector</li>
                        <li class="list-group-item">3. Run Browser</li>
                        <li class="list-group-item">4. Destory Database</li>
                    </ol>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-9">
                <div class="panel panel-default">
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
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default" style="height: 340px;">
                    <div class="panel-header" style="background-color: crimson;">
                        <h4 style="text-align: center; margin-bottom: 0;">Possible Login URLs
                        </h4>
                    </div>
                    <div class="panel-body" style="background-color: black;">
                        <asp:DataList ID="URLListView" runat="server">
                            <HeaderTemplate>
                                <h4>Possible URL List</h4>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item" style="background: #666666; width: 220px; margin: 0 auto;">
                                    <asp:Label runat="server" Text='<%#Eval("PossURL") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-12 col-md-8">
                <div class="panel panel-default">
                    <div class="panel-header" style="background-color: crimson;">
                        <h4 style="text-align: center; margin-bottom: 0;">Browser</h4>
                    </div>
                    <div class="panel-body">
                        <div class="main-login-form">
                            <div class="login-group">
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Username: " />
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="UsrName" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="UsrName" ValidationGroup="Login" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Password: " />
                                    <asp:TextBox runat="server" Enabled="false" CssClass="form-control" ID="Password" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ValidationGroup="Login" />

                                </div>
                            </div>
                            <asp:Button runat="server" CssClass="btn btn-default" Text="Login" OnClick="LoginBtn_Click" ValidationGroup="Login" />
                            <br />
                            <asp:Label runat="server" ID="LoginErrorLbl" />
                        </div>

                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-md-4">
                <div class="panel panel-default" style="height: 219px;">
                    <div class="panel-header" style="background-color: crimson;">
                        <h4 style="text-align: center; margin-bottom: 0;">Injection Code</h4>
                    </div>
                    <div class="panel-body" style="background-color: black;">
                        <asp:DataList runat="server" ID="SQLCodeList">
                            <HeaderTemplate>
                                <h4>SQL Injection Codes</h4>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item" style="background: #666666; width: 220px; margin: 0 auto;">
                                    <asp:Label runat="server" Text='<%#Eval("SQLCode") %>'></asp:Label>
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
