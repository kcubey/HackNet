<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="PartsInfo.aspx.cs" Inherits="HackNet.Game.PartsInfo" %>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
    <style>
        /* Google Fonts */
        @import url(http://fonts.googleapis.com/css?family=Anonymous+Pro);

        /* Global */
        html{
            overflow: hidden;
        }
        body{
          font-family: 'Exo', monospace; 
        }
        .line-1{
            float:left;
            margin: 0 auto;
            overflow: hidden;    
        }

        /* Animation */
        .anim-typewriter{
          animation: typewriter 0.4s steps(3) 1s 1 normal both;
        }
        @keyframes typewriter{
          from{width: 0;}
          to{width: 2em;}
        }
        @keyframes blinkTextCursor{
          from{border-right-color: rgba(255,255,255,.75);}
          to{border-right-color: transparent;}
        }
    </style>
    <div>
        <div style="float:left; width:20%;"> <!-- Left -->
            <asp:Image runat="server" ID="ItemImageLoaded" Width="200px" Height="200px"/>
        </div>
        <div style="float:left; width:76%; color:#83F52C;" > <!-- Right -->
            <asp:Label runat="server" ID="ItemTypeLbl"></asp:Label>
            <br />
            <asp:Label runat="server" ID="ItemName"></asp:Label>
            <br />
            <p style="float:left;" class="line-1 anim-typewriter"><asp:Label runat="server" ID="ItemPrice"></asp:Label></p> 
            <p style="float:left;">&nbsp;Coins</p> 
            <div style="clear:both;"></div>
            <br />
            <br />
            <asp:Label runat="server" ID="ItemDesc"></asp:Label>
            <br />
        </div>
        <div style="clear:both;"></div>
    </div>
    <div style="margin-left:85%;">
    <asp:Button runat="server" ID="BuyBtn" CssClass="btn btn-default"  OnClick="BuyBtn_Click" Text="Buy Now"/>
    </div>
    <br />
    <br />
</asp:Content>
