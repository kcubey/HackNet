<%@ Page Title="Workstation" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Workstation.aspx.cs" Inherits="HackNet.Game.Workstation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <link href="../Content/Tutorial/introjs.css" rel="stylesheet">
    <script>
        function showTutorial() {
            javascript:introJs().start();
        }
    </script>
    <div id="upgradeModel" class="modal fade" role="dialog">
        <div class="modal-dialog" style="color: black;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3>Upgrade Panel</h3>
                </div>
                <div class="modal-body">
                    <asp:Label ID="WorkStnUpgradeName" runat="server" Font-Size="X-Large"></asp:Label>
                    <br />
                    <br />
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Processor: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="200px" ID="ProcessList">                           
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Graphics Card: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="200px" ID="GraphicList">                           
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Memory: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="200px" ID="MemoryList">                           
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Power Supply: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="200px" ID="PowerSupList">                          
                        </asp:DropDownList>
                    </div>
                    <asp:LinkButton runat="server" ID="MarLnkBtn" OnClick="MarLnkBtn_Click" Text="Need a part? Head over to our market now!"></asp:LinkButton>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default">Upgrade</button>
                </div>
            </div>

        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel panel-body">
            <a class="glyphicon glyphicon-question-sign" runat="server" id="HelpBtn" style="float: right; color: greenyellow; font-size: 25px; text-decoration: none;" href="javascript:void(0);" onclick="javascript:introJs().start();"></a>

            <div class="row">
                <div class="col-xs-6 col-md-4">
                    <img src="../Content/Images/workstation.png" />
                    <br />
                    <h4>CPU:</h4>
                    <div class="progress">
                        <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="21" aria-valuemin="0" aria-valuemax="100" style="width: 21%">
                            <asp:Label runat="server" Text="21%"></asp:Label>
                        </div>
                    </div>
                    <h4>Memory:</h4>
                    <div class="progress">
                        <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100" style="width: 50%">
                            <asp:Label runat="server" Text="50%"></asp:Label>
                        </div>
                    </div>
                    <h4>Power:</h4>
                    <div class="progress">
                        <div class="progress-bar progress-bar-success progress-bar-striped active" role="progressbar" aria-valuenow="89" aria-valuemin="0" aria-valuemax="100" style="width: 89%">
                            <asp:Label runat="server" Text="89%"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="col-xs-12 col-md-8">
                    <asp:Label runat="server" ID="WorkstationNameLbl" Font-Size="XX-Large"></asp:Label>


                    <br />
                    <br />
                    <div data-step="1" data-intro="This is how you attack" data-position='right'>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Processor: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="ProcessorLbl" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Graphics Card: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="GraphicLbl" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Memory: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="MemoryLbl" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Power Supply: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="PwsupLbl" Font-Size="Larger"></asp:Label>
                        </div>

                        <hr />
                        <h2>Workstation Stats</h2>
                        <br />
                        <div class="form-group row">
                            <asp:Label runat="server" Text="HP: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="HpattrLabel" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Atk: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="AtkattrLabel" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Def: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="DefattrLabel" Font-Size="Larger"></asp:Label>
                        </div>
                        <div class="form-group row">
                            <asp:Label runat="server" Text="Speed: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                            <asp:Label runat="server" ID="SpeedattrLabel" Font-Size="Larger"></asp:Label>
                        </div>
                    </div>
                    <div data-step="2" data-intro="This is how you attack" data-position='right'>
                        <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Upgrade" Font-Size="Large" data-toggle="modal" data-target="#upgradeModel"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>


    </div>
    <script type="text/javascript" src="../Content/Tutorial/intro.js"></script>
</asp:Content>
