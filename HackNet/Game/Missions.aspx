<%@ Page Title="Mission" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Missions.aspx.cs" Inherits="HackNet.Game.Missions" %>

<asp:Content ID="MissionPageContent" ContentPlaceHolderID="GameContent" runat="server">

    <div id="attackTypeModel" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ID="AttackTypeHeader"></asp:Label>
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
            <div class="col-sm-12 col-md-9" style="background-image: url(../Content/Images/mission.png); background-size: cover; height: 300px;">
                 <asp:LinkButton ID="AttackLink" OnClick="AttackLink_Click" runat="server" Text="Attack" CssClass="btn btn-default"></asp:LinkButton>
            </div>
            <div class="col-sm-12 col-md-3" style="border: 1px solid green;">
                Types of attacks:
                <ul>
                    <li>
                        SQL Injection
                        <asp:Linkbutton runat="server" CssClass="glyphicon glyphicon-question-sign" data-toggle="modal" data-target="#attackTypeModel"/>                                              
                    </li>
                    <li>
                        Cross-site scripting
                    </li>
                    <li>
                        MITM Attack
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
        <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 400px; height: 350px;">
            <asp:Panel ID="LogPanel" runat="server"></asp:Panel>


        </div>
        <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white; padding: ;">
            <asp:Label runat="server" Text="username@HackNet:~#"></asp:Label>
            <asp:TextBox runat="server" BackColor="#091012" BorderStyle="None" Style="min-width: 50%; width: 80%; padding: 5px;"></asp:TextBox>
        </div>

    </div>
</asp:Content>
