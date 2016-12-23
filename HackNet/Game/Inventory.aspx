<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="HackNet.Game.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tab1default" data-toggle="tab">All</a></li>
                    <li><a href="#tab2default" data-toggle="tab">Processor</a></li>
                    <li><a href="#tab3default" data-toggle="tab">Graphic Card</a></li>
                    <li><a href="#tab4default" data-toggle="tab">Memory</a></li>
                    <li><a href="#tab5default" data-toggle="tab">Power Supply</a></li>
                    <li><a href="#tab6default" data-toggle="tab">Boosters</a></li>
                </ul>
            </div>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tab1default">
                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="2" RepeatLayout="Table" Width="500px">
                            
                        </asp:DataList>

                    </div>
                    <div class="tab-pane fade" id="tab2default">Default 2</div>
                    <div class="tab-pane fade" id="tab3default">Default 3</div>
                    <div class="tab-pane fade" id="tab4default">Default 4</div>
                    <div class="tab-pane fade" id="tab5default">Default 5</div>
                    <div class="tab-pane fade" id="tab6default">Default 6</div>
                </div>
            </div>
        </div>
    </div>
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
            <asp:Label runat="server" Text="Item Stat: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="ItemStat"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddItem" CssClass="btn btn-default" OnClick="btnAddItem_Click" Text="Add Item" />
    </div>
</asp:Content>
