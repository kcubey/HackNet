<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Layout.aspx.cs" Inherits="HackNet.Payment.Layout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <header class="main">
        <div class="container wide">
            <div class="content slim">
                <div class="set">
                    <div class="fill">
                        <a class="pseudoshop" href="/">PSEUDO<strong>SHOP</strong></a>
                    </div>

                    <div class="fit">
                        <a class="braintree" href="https://developers.braintreepayments.com/guides/drop-in" target="_blank">Braintree</a>
                    </div>
                </div>
            </div>
        </div>

        <div class="notice-wrapper">
           @{
            string flash = TempData["Flash"] as string;
            if (flash != null)
            {
                <div class="show notice error notice-error">
                    <span class="notice-message">@flash</span>
                </div>
            }   
           }
        </div>
    </header>
    
    @RenderBody()

</asp:Content>
