<%@ Page Title="Workstation" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Workstation.aspx.cs" Inherits="HackNet.Game.Workstation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <div class="panel panel-default">
        <div class="panel panel-body">
            <div class="row">
                <div class="col-xs-6 col-md-4">
                    <img src="../Content/Images/workstation.png" />
                </div>
                <div class="col-xs-12 col-md-8">
                    <asp:Label runat="server" ID="WorkstationNameLbl" Font-Size="X-Large"></asp:Label>
                    <br /><br />
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Processor: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:Label runat="server" ID="ProcessorLbl" Font-Size="Larger"></asp:Label>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Graphics Card: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:Label runat="server" ID="GraphicLbl" Font-Size="Larger" ></asp:Label>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Memory: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:Label runat="server" ID="MemoryLbl" Font-Size="Larger"></asp:Label>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Power Supply: " Font-Size="Larger" CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:Label runat="server" ID="PwsupLbl" Font-Size="Larger"></asp:Label>
                    </div>
                </div>
            </div>
            <hr />
            <h2 style="text-align:center;">Workstation Stats</h2>
        </div>
        

    </div>
</asp:Content>
