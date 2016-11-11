<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="HackNet.Market.Market" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market/market.css" />

    <fieldset>
        <legend>In Game Boosts:</legend>
        2 GB Ram <a href="#">+</a>    5 coin<br>
        Level 5 CPU <a href="#">+</a>        5 coin<br>
        New Charging Port <a href="#">+</a>    1 buck<br>
        High Speed Wifi <a href="#">+</a>    10 buck<br>
    </fieldset>

<br><br>

    <fieldset>
        <legend>Real Money Boosts: </legend>
        10 coins  <a href="#">+</a>    $5<br>
        100 coins  <a href="#">+</a>    $10<br>
        1000 coins <a href="#">+</a>    $20<br>
        10000 coins <a href="#">+</a>    $30<br>

        10 buck <a href="#">+</a>    $1<br>
        100 buck <a href="#">+</a>    $2<br>
        1000 buck <a href="#">+</a>    $3<br>
        10000 buck <a href="#">+</a>    $5<br>
    </fieldset>

        
        ACCORDION: 

        <script>
        var acc = document.getElementsByClassName("accordion");
        var i;

        for (i = 0; i < acc.length; i++) {
            acc[i].onclick = function () {
                this.classList.toggle("active");
                this.nextElementSibling.classList.toggle("show");
            }
        }
    </script>

    <button class="accordion">Section 1</button>
    <div class="panel">
       <p>testing</p>
    </div>

    <button class="accordion">Section 2</button>
    <div class="panel">
        <p>testing</p>
    </div>

       

</asp:Content>
