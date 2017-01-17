<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="HackNet.Game.Currency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />
    <!-- this cssfile can be found in the jScrollPane package -->
    <link rel="stylesheet" type="text/css" href="jquery.jscrollpane.css" />
    <!-- latest jQuery direct from google's CDN -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="jquery.jscrollpane.min.js"></script>

    <style>
        .customscrollbar{

        }
    </style>

    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Market - Currency</h3>
		</div>
		<div class="panel-body">
            <div class="col-xs-3">
                <!-- required for floating -->
                <!-- Nav tabs -->
                <ul class="nav nav-tabs tabs-left fade in active" style="width:20%;">
                    <li class="active"><a href="#All" data-toggle="tab">All</a></li>
                    <li><a href="#processor" data-toggle="tab">Processor</a></li>
                    <li><a href="#graphicscard" data-toggle="tab">Graphics Card</a></li>
                </ul>
            </div>
            <div class="col-xs-9" style="float:left ;">
                <!-- Tab panes -->
                <div class="tab-content">
                    <!--ALL-->
                    <div class="tab-pane fade in active" id="All">
                        <asp:DataList ID="PartsList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='<%#Eval("ItemPic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!--Processor-->
                    <div class="tab-pane fade in" id="processor">
                        <asp:DataList ID="ProcessList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='<%#Eval("ItemPic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!--Graphics Card-->
                    <div class="tab-pane fade in" id="graphicscard">
                        <asp:DataList ID="graphicslist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='<%#Eval("ItemPic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br /><br /><br />

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






    <fieldset>
        <legend>Market</legend>

        <div style="background-color:#434343;">
            <div style="background-color:#ff0000; float:left; width:15%; margin-left:0.5%; height:auto;">
                <h1>Currency</h1>
                    <p><input type="checkbox" name="coins" value="Coins">Coins</p>
                    <p><input type="checkbox" name="bytedollar" value="ByteDollar">ByteDollar</p>
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
