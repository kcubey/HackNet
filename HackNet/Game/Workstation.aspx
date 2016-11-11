<%@ Page Title="Workstation" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Workstation.aspx.cs" Inherits="HackNet.Game.Workstation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <div class="panel panel-default">
        <div class="panel panel-body">
            <div class="col-sm-12 col-md-3">
                 <img src="../Content/Images/workstation.png" />
            </div>
            <div class="col-sm-12 col-md-9">
               <asp:Label runat="server" ID="WorkstationNameLbl" Text="Sample_Text" Font-Size="X-Large"></asp:Label>
                <br /><br />
                <asp:Label runat="server" Text="Processor: "></asp:Label>
                <asp:Label runat="server" ID="ProcessorLbl" Text="I6 Generation 7" Font-Size="Larger"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Graphics Card: "></asp:Label>
                <asp:Label runat="server" ID="GraphicLbl" Text="Nvidia GTX 2010T" Font-Size="Larger"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Memory: "></asp:Label>
                <asp:Label runat="server" ID="MemoryLbl" Text="8GB" Font-Size="Larger"></asp:Label>
                <br />
                <asp:Label runat="server" Text="Power Supply: "></asp:Label>
                <asp:Label runat="server" ID="PwsupLbl" Text="100watts" Font-Size="Larger"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
