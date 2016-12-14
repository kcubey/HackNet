<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="HackNet.Game.Parts" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />

    <fieldset>
        <legend>Market</legend>
            
        <div style="background-color:#434343;">
            <div style="background-color:#ff0000; float:left; width:15%; margin-left:0.5%; height:auto;">
                <h1>Parts</h1>
                    <p><input type="checkbox" name="cpu" value="CPU">CPU</p>
                    <p><input type="checkbox" name="powersupply" value="Power Supply">Power supply</p>
                    <p><input type="checkbox" name="graphics" value="Graphics">Graphics</p>
                    <p><input type="checkbox" name="ram" value="RAM">RAM</p>
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
