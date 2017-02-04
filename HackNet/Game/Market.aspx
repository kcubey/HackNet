<%@ Page Title="Market" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="HackNet.Game.Market" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />

    <style>
        .PartsDiv {
            background: url(../Content/Images/asushardware.jpg) no-repeat 0 0;
            background-color: #4b5320;
            color:#fff;
            font-size:20px;
            float: left;
            width: 48.92%;
            margin-left: 1%;
            padding-top: 200px;
            padding-bottom: 200px;
            /*padding-left:22%;*/
            -webkit-transition-duration: 1s;
            -moz-transition-duration: 1s;
            -o-transition-duration: 1s;
            transition-duration: 1s;
        }
        .PartsDiv:hover {
            background: url(../Content/Images/comhardwarepic.jpg) no-repeat fixed center;
            background-color:#000000; 
            color:#fff;
            font-size:20px;
            -webkit-transition: background-color 0.5s ease-out;
            -moz-transition: background-color 0.5s ease-out;
            -o-transition: background-color 0.5s ease-out;
            transition: background-color 0.5s ease-out;
        }
        .CurrencyDiv {
            background: url(../Content/Images/bitcoin-mining.jpg) no-repeat 0 0;
            background-color:#505823; 
            color:#fff;
            font-size:20px;
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
            background: url(../Content/Images/greenlights.jpg) no-repeat fixed center;
            background-color:#000000; 
            color:#fff;
            font-size:20px;
            -webkit-transition: background-color 0.5s ease-out;
            -moz-transition: background-color 0.5s ease-out;
            -o-transition: background-color 0.5s ease-out;
            transition: background-color 0.5s ease-out;
        }
    </style>

    <fieldset>
        <legend>Market</legend>
        
        
        <!--Left div--><!--  GeneratedSite.css textarea max width  -->
        <asp:Button Style="max-width:none;" runat="server" class="PartsDiv" Text="Parts" OnClick="LnkPts" />
        <!--Right div--><!--  GeneratedSite.css textarea max width  -->
        <asp:Button Style="max-width:none;" runat="server" class="CurrencyDiv" Text="Currency" OnClick="LnkCurr"/>

        <!--Trying ImageButton-->
        <!--
        <asp:ImageButton ID="imgbtn1" Style="max-width:none;" runat="server" ImageUrl="../Content/Images/console.png" Text="Parts" OnClick="LnkPts" />
        <asp:ImageButton ID="imgbtn2" Style="max-width:none;" runat="server" ImageUrl="../Content/Images/console.png" Text="Currency" OnClick="LnkCurr"/>
        -->
        
    </fieldset>

</asp:Content>
