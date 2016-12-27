<%@ Page Title="Mission" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Missions.aspx.cs" Inherits="HackNet.Game.Missions" %>

<asp:Content ID="MissionPageContent" ContentPlaceHolderID="GameContent" runat="server">
    <link href="../Content/Tutorial/introjs.css" rel="stylesheet">
    <script>
        function showPopupattackinfo() {
            $('#attackTypeModel').modal('show');
        }
        function showPopupattacksummary() {
            $('#attackSummaryModel').modal('show');
        }
    </script>
    <div id="attackTypeModel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ID="AttackTypeHeaderLbl" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-md-8">
                            <asp:Label runat="server" ID="AttackTypeInfo" ForeColor="Black"></asp:Label>
                        </div>
                        <div class="col-xs-6 col-md-4">
                            <asp:Image runat="server" ID="AtkTypePic1" Width="150px" Height="150px" />
                        </div>
                    </div>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div id="attackSummaryModel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ID="MissionTitleLbl" ForeColor="Black"></asp:Label>
                </div>
                <div class="modal-body" style="color: black;">
                    <asp:Label runat="server" ID="MisDesLbl"></asp:Label>

                    <p>Use a password attack</p>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton CssClass="btn btn-default" runat="server" OnClick="AttackLink_Click" Text="Attack" />
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel panel-body">
            <a class="glyphicon glyphicon-question-sign" style="float: right; color: greenyellow; font-size: 25px; text-decoration: none;" href="javascript:void(0);" onclick="javascript:introJs().start();"></a>
            <div class="col-sm-12 col-md-9">
                <div class="form-group row" data-step="1" data-intro="This is how you attack" data-position='right'>
                    <asp:Label runat="server" Text="Region of attack: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                    <asp:DropDownList runat="server" AutoPostBack="true" ID="regatkList" OnSelectedIndexChanged="regatkList_SelectedIndexChanged"
                        CssClass="col-xs-4 col-form"
                        BackColor="Black">
                        <asp:ListItem Value="-1" Text="--Select Region of attack--"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Local"></asp:ListItem>
                        <asp:ListItem Value="1" Text="America"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Europe"></asp:ListItem>
                        <asp:ListItem Value="3" Text="South Asia"></asp:ListItem>
                    </asp:DropDownList>

                </div>
                <div class="row" data-step="2" data-intro="Ok, wasn't that fun?" data-position='right' style="background-image: url(../Content/Images/mission.png); background-size: cover; height: 300px;">
                    <asp:DataList ID="AtkListView" runat="server" RepeatLayout="Table" Width="100%">
                        <HeaderTemplate>
                            <asp:Table runat="server" CssClass="table">
                                <asp:TableHeaderRow>
                                    <asp:TableHeaderCell Font-Bold="true" Font-Size="Large">IP address</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true" Font-Size="Large">Mission Name</asp:TableHeaderCell>
                                    <asp:TableHeaderCell Font-Bold="true" Font-Size="Large">Recommended Level</asp:TableHeaderCell>
                                    <asp:TableHeaderCell></asp:TableHeaderCell>
                                </asp:TableHeaderRow>
                            </asp:Table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Table runat="server" CssClass="table">
                                <asp:TableRow>
                                    <asp:TableCell Width="180px">
                                                <asp:Label runat="server" Text='<%#Eval("IP Address") %>'></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="250px">
                                                <asp:Label runat="server" Text='<%#Eval("Mission Name") %>'></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell Width="300px">
                                                <asp:Label runat="server" Text='<%#Eval("Recommended Level") %>'></asp:Label>
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:LinkButton ID="ViewMis" runat="server"
                                            Text="View" CssClass="btn btn-default" CommandArgument='<%# Eval("MissionId") %>'
                                            OnCommand="ViewMis_Command"></asp:LinkButton>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
            </div>
            <div class="col-sm-12 col-md-3" data-step="4" data-intro="Ok, wasn't that fun?" data-position='right'>
                <h4>Types of attacks:</h4>
                <asp:DataList ID="TypeAtkListView" runat="server">
                    <ItemTemplate>
                        <li class="list-group-item" style="background: #666666; width:250px;">
                            <asp:Label runat="server" Text='<%#Eval("AttackName") %>'></asp:Label>
                            <asp:LinkButton runat="server"
                                OnCommand="abtAtkInfo_Command"
                                CommandArgument='<%#Eval("AttackId") %>'
                                CssClass="glyphicon glyphicon-question-sign"
                                ForeColor="GreenYellow" Style="float: right;"></asp:LinkButton>
                        </li>
                    </ItemTemplate>
                </asp:DataList>
            </div>
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
            <asp:TextBox runat="server" ID="AtkTextBx" OnTextChanged="AtkTextBx_TextChanged" BackColor="#091012" BorderStyle="None" Style="min-width: 50%; width: 80%; padding: 5px;"></asp:TextBox>
        </div>
        <asp:Label runat="server" ID="errorLbl"></asp:Label>
    </div>

    <div class="container-fluid" style="color: black; background-color: gray;">
        <h2>Attack Info Editor</h2>
        <div class="form-group row">
            <asp:Label runat="server" Text="Attack Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="AtkName"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Attack Information: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="AtkInfo" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Attack Image1: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:FileUpload ID="UploadAttack1" runat="server" />
        </div>
        <asp:Button runat="server" ID="btnAtkInfo" CssClass="btn btn-default" OnClick="btnAtkInfo_Click" />
    </div>

    <div class="container-fluid" style="color: black; background-color: gray;">
        <h2>Mission Editor</h2>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisName"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Desc: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisDesc" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="AtkTypeList">
                <asp:ListItem Value="0">AtkTypPwdAtks</asp:ListItem>
                <asp:ListItem Value="1">AtkTypDdos</asp:ListItem>
                <asp:ListItem Value="2">AtkTypMITM</asp:ListItem>
                <asp:ListItem Value="3">AtkTypSQL</asp:ListItem>
                <asp:ListItem Value="4">AtkTypXSS</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Recommend Level: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="RecomLvlList">
                <asp:ListItem Value="0">Lvl1to5</asp:ListItem>
                <asp:ListItem Value="1">Lvl6to10</asp:ListItem>
                <asp:ListItem Value="2">Lvl11to15</asp:ListItem>
                <asp:ListItem Value="3">Lvl16to20</asp:ListItem>
            </asp:DropDownList>
        </div>
        <asp:Button runat="server" ID="btnAddMis" CssClass="btn btn-default" OnClick="btnAddMis_Click" Text="Add Item" />
    </div>

    <script type="text/javascript" src="../Content/Tutorial/intro.js"></script>
</asp:Content>
