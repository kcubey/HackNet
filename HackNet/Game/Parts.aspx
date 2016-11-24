<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="HackNet.Game.Parts" %>
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
            padding-left:22%;
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
            padding-left:22%;
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
        <!--Left div-->
        <div class="PartsDiv">
            <p>Parts</p>
        </div>
        <!--Right div-->
        <div class="CurrencyDiv">
            <p>Currency</p>
        </div>
        <div style="clear:both;"></div>
    </fieldset>

</asp:Content>
