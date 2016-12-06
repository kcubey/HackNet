<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="HackNet.Game.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <style>
        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        .tab {
            border: 1px solid black;
            background-image: url(images/navigation.jpg);
            background-repeat: repeat-x;
            color: White;
            padding: 5%;
            background-color:black;
        }

        .tabcontents {
            border: 1px solid black;
            padding: 10px;
            width: 100%;
            height: 500px;
            background-color: white;
            color:black;
        }
    </style>
    <asp:Menu ID="InventoryMain" runat="server" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
        StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" OnMenuItemClick="Menu1_MenuItemClick">
        <Items>
            <asp:MenuItem Text="All" Value="0" Selected="true"></asp:MenuItem>
            <asp:MenuItem Text="CPU" Value="1"></asp:MenuItem>
            <asp:MenuItem Text="Graphic Card" Value="2"></asp:MenuItem>
            <asp:MenuItem Text="Memory" Value="3"></asp:MenuItem>
            <asp:MenuItem Text="Power Supply" Value="4"></asp:MenuItem>

        </Items>
    </asp:Menu>
    <div class="tabcontents">
        <asp:MultiView ID="InventoryView" ActiveViewIndex="0" runat="server">
            <asp:View ID="Tab1" runat="server">
                This is a View 1.
            </asp:View>
            <asp:View ID="Tab2" runat="server">
                This is a View 2.
            </asp:View>
            <asp:View ID="Tab3" runat="server">
                This is a View 3.
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
