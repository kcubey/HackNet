<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Market.aspx.cs" Inherits="HackNet.Market.Market" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market/market.css" />
    <link rel="stylesheet" href="/payment/backend/redirectimagebutton.css" />

    <h1>Market</h1><hr />
    <asp:Label ID="warning" runat="server" Text="* WARNING *" ForeColor="Red" CssClass="btn btn-success" Font-Bold="True" Font-Size="Large" BackColor="Black"></asp:Label>
    <p>These premium packages require the use of REAL money, 
        <asp:LinkButton ID="virMarkt" runat="server" CssClass="btn btn-success" postbackurl="~/Game/Market1.aspx">click to go to the virtual market</asp:LinkButton></p>

        <asp:LinkButton ID="panelNew" class="redirectButton" runat="server" postbackurl="/payment/payment.aspx" Font-Underline="False">
            <asp:Image ID="blogImg" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <!--http://counterintuity.com/wp-content/uploads/2015/08/iStock_000015286795Medium.jpg-->
            <br /><br />
            <asp:Label ID="blogLbl" CssClass="redirectLbl" runat="server" Text="Package A - SGD$10"></asp:Label></asp:LinkButton>

        <asp:LinkButton ID="panelNew2" class="redirectButton" runat="server" postbackurl="/payment/payment.aspx" Font-Underline="False">
            <asp:Image ID="sellImg" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <!--http://myob.co.nz/blog/files/2014/09/140922_selling.jpg-->
            <br /><br />
            <asp:Label ID="sellLbl" CssClass="redirectLbl" runat="server" Text="Package B - SGD$100"></asp:Label></asp:LinkButton>

        <asp:LinkButton ID="LinkButton1" class="redirectButton" runat="server" postbackurl="/payment/payment.aspx" Font-Underline="False">
            <asp:Image ID="Image1" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <!--http://myob.co.nz/blog/files/2014/09/140922_selling.jpg-->
            <br /><br />
            <asp:Label ID="Label1" CssClass="redirectLbl" runat="server" Text="Package C - SGD$50"></asp:Label></asp:LinkButton>

        <asp:LinkButton ID="LinkButton2" class="redirectButton" runat="server" postbackurl="/payment/payment.aspx" Font-Underline="False"> 
            <asp:Image ID="Image2" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <!--http://myob.co.nz/blog/files/2014/09/140922_selling.jpg-->
            <br /><br />
            <asp:Label ID="Label2" CssClass="redirectLbl" runat="server" Text="Package D - SGD$20"></asp:Label></asp:LinkButton>

        <asp:LinkButton ID="LinkButton3" class="redirectButton" runat="server" postbackurl="/payment/payment.aspx" Font-Underline="False"> 
            <asp:Image ID="Image3" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
            <!--http://myob.co.nz/blog/files/2014/09/140922_selling.jpg-->
            <br /><br />
            <asp:Label ID="Label3" CssClass="redirectLbl" runat="server" Text="Package E - SGD$350"></asp:Label></asp:LinkButton>

    <br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
         </div>
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
