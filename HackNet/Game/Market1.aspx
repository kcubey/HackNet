<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Market1.aspx.cs" Inherits="HackNet.Market.Market1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />

    <style>
        .PartsDiv {
            background-color:#4b5320; 
            float:left; 
            width:48.92%; 
            margin-left:1%; 
            padding-top:200px; 
            padding-bottom:200px;
            /*padding-left:22%;*/
            -webkit-transition-duration: 1s;
            -moz-transition-duration: 1s;
            -o-transition-duration: 1s;
            transition-duration: 1s;
        }
        .PartsDiv:hover {
            background-color:#000000; 
            -webkit-transition: background-color 0.5s ease-out;
            -moz-transition: background-color 0.5s ease-out;
            -o-transition: background-color 0.5s ease-out;
            transition: background-color 0.5s ease-out;
        }
        .CurrencyDiv {
            background-color:#505823; 
            float:right; 
            width:48.92%; 
            margin-right:1%; 
            padding-top:200px; 
            padding-bottom:200px; 
            /*padding-left:22%;*/
            -webkit-transition-duration: 1s;
            -moz-transition-duration: 1s;
            -o-transition-duration: 1s;
            transition-duration: 1s;
        }
        .CurrencyDiv:hover {
            background-color:#000000; 
            -webkit-transition: background-color 0.5s ease-out;
            -moz-transition: background-color 0.5s ease-out;
            -o-transition: background-color 0.5s ease-out;
            transition: background-color 0.5s ease-out;
        }
    </style>

    <fieldset>
        <legend>Market</legend>
        <!--
        <div>Parts</div>
            <div>CPU</div>
            <div>Power Supply</div>
            <div>Graphics</div>
            <div>RAM</div>
        <div>Currency</div>
            <div>Coins</div>
            <div>ByteDollar</div>
        -->
        
        <!--
        <div runat="server" class="PartsDiv" onclick="LnkPts">
            <p>Parts</p>
        </div>
        <div runat="server" class="CurrencyDiv" onclick="LnkCurr">
            <p>Currency</p>
        </div>
        <div style="clear:both;"></div>
        -->

        <!--Left div--><!--  GeneratedSite.css textarea max width  -->
        <asp:Button runat="server" class="PartsDiv" Text="Parts" OnClick="LnkPts"/>
        <!--Right div--><!--  GeneratedSite.css textarea max width  -->
        <asp:Button runat="server" class="CurrencyDiv" Text="Currency" OnClick="LnkCurr"/>

        
    </fieldset>

</asp:Content>
