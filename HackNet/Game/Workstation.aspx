<%@ Page Title="Workstation" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Workstation.aspx.cs" Inherits="HackNet.Game.Workstation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <div id="upgradeModel" class="modal fade" role="dialog">
        <div class="modal-dialog" style="color:black;">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h3>Upgrade Model</h3>
                </div>
                <div class="modal-body">
                  <asp:Label runat="server" Font-Size="XX-Large">Workstation Name</asp:Label>
                     <div class="form-group row">
                        <asp:Label runat="server" Text="Processor: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="150px">
                            <asp:ListItem>I7</asp:ListItem>
                            <asp:ListItem>I5</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Graphics Card: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DropDownList runat="server" Width="150px">
                            <asp:ListItem>Mevidia</asp:ListItem>
                            <asp:ListItem>Nividia</asp:ListItem>
                            <asp:ListItem>Wovidia</asp:ListItem>
                            <asp:ListItem>Uvidia</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Memory: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                       <asp:DropDownList runat="server" Width="150px">
                            <asp:ListItem>1GB</asp:ListItem>
                            <asp:ListItem>2GB</asp:ListItem>
                            <asp:ListItem>3GB</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Power Supply: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                       <asp:DropDownList runat="server" Width="150px">
                            <asp:ListItem>1W</asp:ListItem>
                            <asp:ListItem>100W</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default">Upgrade</button>
                </div>
            </div>

        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel panel-body">
            <asp:LinkButton runat="server" CssClass="glyphicon glyphicon-question-sign" Style="float: right;" Font-Size="X-Large" Font-Strikeout="False" ForeColor="Lime" />
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
                    <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Upgrade" Font-Size="Large"  data-toggle="modal" data-target="#upgradeModel"></asp:LinkButton>
                </div>
            </div>
        </div>


    </div>
</asp:Content>
