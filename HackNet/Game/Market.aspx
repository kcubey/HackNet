﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="HackNet.Market.Market" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/payment/backend/redirectimagebutton.css" />

    <script>
        $('#pages').change(function (ev) {
            var total = $('#pages').val() * 2;
            $('#totall').html(total);
        });

        $('#pages').keyup(function (ev) {
            var total = $('#pages').val() * 2;
            $('#totall').html(total);
        });

        $('#buckTextBox1').keyup(function(ev){
            var total = $('#buckTextBox1').val() * 2;
            document.getElementById("coinTotal").innerHTML = total;
            // $('#coinTotal').html(total);
        });

        $('#buckTextBox1').change(function (ev) {
           var total = $('#buckTextBox1').val() * 2;
           document.getElementById("coinTotal").innerHTML = total;
           // $('#coinTotal').html(total);
        });
    </script>

    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Market - Currency</h3>
		</div>
		<div class="panel-body">
            <div id="marketArea">
			    <asp:Label ID="warning" runat="server" Text="* WARNING *" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
                <p>These premium packages require the use of REAL money.</p> 

                <asp:LinkButton ID="LinkButton1" class="redirectButton" runat="server" onclick="RedirectButton_Click" Font-Underline="False">
                    <asp:Image ID="Image1" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                    <br /><br />
                    <asp:Label ID="Label1" CssClass="redirectLbl" runat="server" Text="Package A - SGD$10"></asp:Label></asp:LinkButton>

                <asp:LinkButton ID="LinkButton2" class="redirectButton" runat="server" postbackurl="/payment/reauth.aspx" Font-Underline="False">
                    <asp:Image ID="Image2" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                    <br /><br />
                    <asp:Label ID="Label2" CssClass="redirectLbl" runat="server" Text="Package B - SGD$100"></asp:Label></asp:LinkButton>

                <asp:LinkButton ID="LinkButton3" class="redirectButton" runat="server" postbackurl="/payment/reauth.aspx" Font-Underline="False">
                    <asp:Image ID="Image3" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                    <br /><br />
                    <asp:Label ID="Label3" CssClass="redirectLbl" runat="server" Text="Package C - SGD$50"></asp:Label></asp:LinkButton>

                <asp:LinkButton ID="LinkButton4" class="redirectButton" runat="server" postbackurl="/payment/reauth.aspx" Font-Underline="False"> 
                    <asp:Image ID="Image4" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                    <br /><br />
                    <asp:Label ID="Label4" CssClass="redirectLbl" runat="server" Text="Package D - SGD$20"></asp:Label></asp:LinkButton>

                <asp:LinkButton ID="LinkButton5" class="redirectButton" runat="server" postbackurl="/payment/reauth.aspx" Font-Underline="False"> 
                    <asp:Image ID="Image5" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                    <br /><br />
                    <asp:Label ID="Label5" CssClass="redirectLbl" runat="server" Text="Package E - SGD$350"></asp:Label></asp:LinkButton>
            </div>
            
            <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
            
            <div id="convertArea">
                <hr />
                original w rangevalidator
                <br />Bucks:
                <asp:TextBox ID="buckTextBox" CssClass="form-control" runat="server" ForeColor="Black"></asp:TextBox>
                <asp:RangeValidator ID="buckValidator" ControlToValidate="buckTextBox" 
                MinimumValue="0" Type="Integer" runat="server"
            ErrorMessage="* Please enter a valid number" ForeColor="Red"></asp:RangeValidator>

                Coins: 
                <asp:TextBox ID="coinTextBox"  CssClass="form-control" runat="server" ForeColor="Black" ReadOnly="True"></asp:TextBox>

                <hr />
                change to JS math
                Bucks: 
                <asp:TextBox ID="buckTextBox1"  type="number" clientidmode="Static" CssClass="form-control" runat="server" ForeColor="Black"></asp:TextBox>
                <br />Coins: <asp:Label ID="coinLabel" runat="server" Text=""></asp:Label>    

                <hr />
                demo code
                Pages: <input type="number" class="form-control" id="pages" step="1"/>
                <br/><br/>
                Total: <span id="totall">0.00</span>
    
                <br /><br />

                <asp:Button ID="CheckValueButton" runat="server" CssClass="btn btn-success" Text="Check Value" onclick="CheckValueButton_Click"/>
                <asp:Button ID="ConversionButton" runat="server" CssClass="btn btn-success" Text="Convert" OnClick="ConversionButton_Click"/>
                </div>
        </div>
	</div>

        

    <!-- Item repeater

        ===For Current===
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <asp:LinkButton ID="LinkButton1" class="redirectButton" runat="server" postbackurl="/payment/reauth.aspx" Font-Underline="False">
            <asp:Image ID="Image1" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <br /><br />
            <asp:Label ID="pkgCLbl" CssClass="redirectLbl" runat="server" Text="Package C - SGD$50"></asp:Label></asp:LinkButton>
        </ItemTemplate>
    </asp:Repeater>
    
        ===Example===
        <asp:Repeater ID="repPosts" runat="server">
            <ItemTemplate>
                <figure class="postSnip">
                    <img class="postImg" runat="server" src='<[]%# Eval("PostImage") %>' >
                        <img class="postImg" src="https://67.media.tumblr.com/93446adc3eeb10d4a492f8ce2eb70760/tumblr_o8pil6Czqe1rk63wco2_500.gif">
                    <figcaption class="postTitle">
                        <asp:Label ID="lblPostTitle" runat="server" Text='<[]%# Eval("PostName") %>'
                            onclick='window.location="IdeasPost?PostId=<[]%# Eval("PostId") %>"' />
                       <a href='IdeasPost?PostId=<[]%# Eval("PostId") %>' 
                           title='<[]%# Eval("PostName") %>' id="postTitleAnchor"><[]%# Eval("PostName") %></a>
                    </figcaption>
                    <figcaption class="postContent">
                        <asp:Label ID="lblPostDescription" runat="server" Text='<[]%# Eval("PostDescription") %>' />
                    </figcaption>
	            </figure>
            </ItemTemplate>
        </asp:Repeater>

        *** Remove[] brackets
    -->
    
    
    <!--
        
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
    -->
       

</asp:Content>