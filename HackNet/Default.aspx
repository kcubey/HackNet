<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HackNet._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
   <div class="bottomNav" style="background-color:#222222; height:50px; border-bottom-left-radius:5px;border-bottom-right-radius:5px;">
        <div class="container">             
                <div class="navbar-collapse collapse">
                    <ul class="nav navbar-nav">
                        <li><a runat="server" href="~/">Home</a></li>
                        <li><a runat="server" href="~/About">Inventory</a></li>
                        <li><a runat="server" href="~/Contact">Market</a></li>
                    </ul>
                </div>
            </div>
   </div>
    <div class="jumbotron">
        
    </div>

  

</asp:Content>
