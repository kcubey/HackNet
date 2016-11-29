<%@ Page Title="Mission" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Missions.aspx.cs" Inherits="HackNet.Game.Missions" %>

<asp:Content ID="MissionPageContent" ContentPlaceHolderID="GameContent" runat="server">
    <script>
        function showPopupattackinfo() {
            $('#attackTypeModel').modal('show');
        }
    </script>
    <div id="attackTypeModel" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ID="AttackTypeHeader" ForeColor="Black"></asp:Label>
                </div>
                <div class="modal-body">
                    <p>Some text in the modal.</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>


    <div class="panel panel-default">
        <div class="panel panel-body">
            <div class="col-sm-12 col-md-9">
                <div class="form-group row">
                    <asp:Label runat="server" Text="Region of attack: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                    <asp:DropDownList runat="server" ID="regatkList"
                        CssClass="col-xs-3 col-form"
                        BackColor="Black">
                    </asp:DropDownList>

                </div>
                <div class="row" style="background-image: url(../Content/Images/mission.png); background-size: cover; height: 300px;">
                    <asp:Table runat="server" CssClass="table" ForeColor="Black" Font-Bold="true">
                        <asp:TableHeaderRow runat="server" Font-Size="Large">
                            <asp:TableCell>IP address</asp:TableCell>
                            <asp:TableCell>Mission Type</asp:TableCell>
                            <asp:TableCell>Number of Firewalls</asp:TableCell>
                            <asp:TableCell>Recommended Level</asp:TableCell>
                        </asp:TableHeaderRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell>192.168.10.2</asp:TableCell>
                            <asp:TableCell>Denial of service attack</asp:TableCell>
                            <asp:TableCell>10</asp:TableCell>
                            <asp:TableCell>1</asp:TableCell>
                            <asp:TableCell><asp:Button runat="server" Text="Attack"/></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell>192.168.10.2</asp:TableCell>
                            <asp:TableCell>Man In the Middle attack</asp:TableCell>
                            <asp:TableCell>10</asp:TableCell>
                            <asp:TableCell>1</asp:TableCell>
                            <asp:TableCell><asp:Button runat="server" Text="Attack"/></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                </div>
            </div>


            <div class="col-sm-12 col-md-3">
                <h4>Types of attacks:</h4>
                <ul class="list-group">
                    <li class="list-group-item" style="background:#666666;">                    
                        Password Attack
                        <asp:LinkButton runat="server" OnCommand="abtAtkInfo_Command" CommandArgument="Password Attack"                 
                            CssClass="glyphicon glyphicon-question-sign" 
                            ForeColor="GreenYellow" style="float:right;" />                       
                    </li>
                    <li class="list-group-item" style="background:#666666;">
                        Denial-of-Service Attack
                        <asp:LinkButton runat="server" OnCommand="abtAtkInfo_Command" CommandArgument="Denial-of-Service Attack"
                            CssClass="glyphicon glyphicon-question-sign" 
                            data-toggle="modal" data-target="#attackTypeModel" ForeColor="GreenYellow" style="float:right;" /> 
                    </li>
                    <li class="list-group-item" style="background:#666666;">
                        Man-in-the-Middle Attack
                        <asp:LinkButton runat="server" OnCommand="abtAtkInfo_Command" CommandArgument="Man-in-the-Middle Attack"
                            CssClass="glyphicon glyphicon-question-sign" 
                            data-toggle="modal" data-target="#attackTypeModel" ForeColor="GreenYellow" style="float:right;" /> 
                    </li>
                    <li class="list-group-item" style="background:#666666;">
                        SQL Injection Attack
                        <asp:LinkButton runat="server" OnCommand="abtAtkInfo_Command" CommandArgument="SQL Injection Attack"
                            CssClass="glyphicon glyphicon-question-sign" 
                            data-toggle="modal" data-target="#attackTypeModel" ForeColor="GreenYellow" style="float:right;" /> 
                    </li>
                    <li class="list-group-item" style="background:#666666;">
                        Cross Site Scripting Attack
                        <asp:LinkButton runat="server" OnCommand="abtAtkInfo_Command" CommandArgument="Cross Site Scripting Attack"
                            CssClass="glyphicon glyphicon-question-sign" 
                            data-toggle="modal" data-target="#attackTypeModel" ForeColor="GreenYellow" style="float:right;" /> 
                    </li>
                </ul>
                



            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-header" style="background-color: grey;">
            <h4 style="text-align: center; margin-bottom: 0;">
                <asp:Label runat="server" Text="user"></asp:Label>@HackNetHost:~
            </h4>
        </div>
        <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 400px; height: 250px;">
            <asp:Panel ID="LogPanel" runat="server"></asp:Panel>


        </div>
        <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white; padding: ;">
            <asp:Label runat="server" Text="username@HackNet:~#"></asp:Label>
            <asp:TextBox runat="server" BackColor="#091012" BorderStyle="None" Style="min-width: 50%; width: 80%; padding: 5px;"></asp:TextBox>
        </div>

    </div>
</asp:Content>
