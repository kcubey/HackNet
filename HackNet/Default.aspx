<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HackNet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="row">
            <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px red solid;">
                <div class="row">
                    <div class="col-xs-6 col-md-4" style="border: 1px solid black; width: 100px; height: 100px;">
                        Profile pic
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-8">
                        <div class="row">
                            <div class="col-xs-12 col-sm-6 col-md-8">
                                <asp:Label runat="server">Name</asp:Label>
                            </div>
                            <div class="col-xs-6 col-md-4">
                                <asp:Label runat="server">Level: ??</asp:Label>
                            </div>
                        </div>

                        <div class="progress">
                            <div class="progress-bar progress-bar-striped active" role="progressbar" aria-valuenow="70" aria-valuemin="0" aria-valuemax="100" style="width: 70%">
                                <asp:Label runat="server" Text="7000/10000XP (70%)"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-md-4" style="border: 1px dotted blue;">
                Medals
                <asp:BulletedList runat="server">
                    <asp:ListItem>
                       Script kiddie
                    </asp:ListItem>
                </asp:BulletedList>
            </div>
        </div>
    </div>

    <div class="jumbotron" style="background:none;">
        <div class="row">
            <div class="col-xs-12 col-md-8" style="border: 1px solid black;">Mission stuff</div>
            <div class="col-xs-6 col-md-4" style="border: 1px solid green;">Events?</div>
        </div>
    </div>


</asp:Content>
