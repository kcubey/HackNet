<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="HackNet.Game.Parts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />

    <fieldset>
        <legend>Market</legend>

        <div style="background-color: #434343;">
            <div style="background-color: #ff0000; float: left; width: 15%; margin-left: 0.5%; height: auto;">
                <h1>Parts</h1>
                <p>
                    <input type="checkbox" name="cpu" value="CPU">CPU</p>
                <p>
                    <input type="checkbox" name="powersupply" value="Power Supply">Power supply</p>
                <p>
                    <input type="checkbox" name="graphics" value="Graphics">Graphics</p>
                <p>
                    <input type="checkbox" name="ram" value="RAM">RAM</p>
            </div>
            <div class="customscrollbar" style="background-color: #4cff00; float: left; width: 82%; padding-left: 1%; margin-left: 1%; margin-right: 0.5%; height: 400px; overflow: scroll; overflow-x: hidden;">
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ff6a00;">randombox left</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #bf4646;">randombox mid</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ffd800;">randombox right</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ff6a00;">randombox left</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #bf4646;">randombox mid</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ffd800;">randombox right</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ff6a00;">randombox left</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #bf4646;">randombox mid</div>
                <div style="float: left; width: 31%; height: 230px; margin: 1%; background-color: #ffd800;">randombox right</div>
                <div style="clear: both;"></div>
            </div>
            <div style="clear: both;"></div>
        </div>
    </fieldset>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <fieldset>
        <h2>Add New Market Listing</h2>
        <span>Title</span>
        <asp:TextBox ID="Listingtitle" runat="server" CssClass="form-control" placeholder="Listing Title" />
        <span>Description</span>
        <asp:TextBox ID="ListingDescription" runat="server" CssClass="form-control" placeholder="Listing Description" />
        <span>Price</span>
        <asp:TextBox ID="ListingPrice" runat="server" CssClass="form-control" placeholder="Listing Description" />
        <asp:Button runat="server" ID="btnAddListing" CssClass="btn btn-default" OnClick="btnAddListing_Click" Text="Add Listing" />

    </fieldset>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <asp:DataList ID="PartsList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
        <ItemTemplate>
            <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
            <br />
            <asp:Image ID="itemImg" runat="server"
                Width="200px" Height="200px"
                ImageUrl='<%#Eval("ItemPic")%>' />
            <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
        </ItemTemplate>
    </asp:DataList>

    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />

    <div class="container-fluid" style="color: black; background-color: gray;">
        <h2>Item Editor</h2>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="ItemName"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="ItemTypeList">
                <asp:ListItem Value="1">Processor</asp:ListItem>
                <asp:ListItem Value="4">Graphic Card</asp:ListItem>
                <asp:ListItem Value="2">Memory</asp:ListItem>
                <asp:ListItem Value="3">Power Supply</asp:ListItem>
                <asp:ListItem Value="0">Booster</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Image: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:FileUpload ID="UploadPhoto" runat="server" />
            <asp:Image ID="imgViewFile" runat="server" />
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Description: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="ItemDesc" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Price: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="ItemPrice"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Item Bonus: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="ItemStat"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddItem" CssClass="btn btn-default" OnClick="btnAddItem_Click" Text="Add Item" />
    </div>
</asp:Content>
