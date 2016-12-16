<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="HackNet.Game.Currency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />
    <!-- this cssfile can be found in the jScrollPane package -->
    <link rel="stylesheet" type="text/css" href="jquery.jscrollpane.css" />
    <!-- latest jQuery direct from google's CDN -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="jquery.jscrollpane.min.js"></script>

    <style>
        .customscrollbar{

        }
    </style>

    <fieldset>
        <legend>Market</legend>

        <div style="background-color:#434343;">
            <div style="background-color:#ff0000; float:left; width:15%; margin-left:0.5%; height:auto;">
                <h1>Currency</h1>
                    <p><input type="checkbox" name="coins" value="Coins">Coins</p>
                    <p><input type="checkbox" name="bytedollar" value="ByteDollar">ByteDollar</p>
            </div>
            <div class="customscrollbar" style="background-color:#4cff00; float:left; width:82%; padding-left:1%; margin-left:1%; margin-right:0.5%; height:400px; overflow:scroll; overflow-x:hidden;">
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="clear:both;"></div>
            </div>
            <div style="clear:both;"></div>
        </div>
    </fieldset>

</asp:Content>
