<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="HackNet.Game.Parts" %>

<asp:Content ID="HeadCt" ContentPlaceHolderID="GameHeadPH" runat="server">
    <style>
        .nav-tabs > li > a{
            color:limegreen;
        }
        .nav-tabs > li.active > a,
        .nav-tabs > li.active > a:hover,
        .nav-tabs > li.active > a:focus {
            background-color:rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
        }

        .nav > li a:hover {
            background-color: rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
        }
        .viewMoreButton {
            color:#B6C5BE;
        }
        .viewMoreButton:hover {
            color:#fff;
            text-decoration:none;
        }
        .viewMoreButton:focus {
            color:#fff;
            text-decoration:none;
        }
        .viewMoreButton:visited {
            color:#fff;
            text-decoration:none;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">


    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Market - Parts</h3>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-3">
                    <!-- required for floating -->
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs tabs-left" style="width: 0%;">
                        <li class="active"><a href="#All" data-toggle="tab">All</a></li>
                        <li><a href="#processor" data-toggle="tab">Processor</a></li>
                        <li><a href="#graphicscard" data-toggle="tab">Graphics Card</a></li>
                        <li><a href="#memory" data-toggle="tab">Memory</a></li>
                        <li><a href="#powersupply" data-toggle="tab">Power Supply</a></li>
                        <li><a href="#booster" data-toggle="tab">Booster</a></li>
                    </ul>
                </div>
                <div class="col-xs-9" style="float: left; height: 580px; overflow: scroll; overflow-x: hidden;">
                    <!-- Tab panes -->
                    <div class="tab-content">
                        <!--ALL-->
                        <div class="tab-pane fade in active" id="All">
                            <asp:DataList ID="PartsList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 4px; margin-bottom: 20px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton CssClass="viewMoreButton" runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--Processor-->
                        <div class="tab-pane fade in" id="processor">
                            <asp:DataList ID="ProcessList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--Graphics Card-->
                        <div class="tab-pane fade in" id="graphicscard">
                            <asp:DataList ID="graphicslist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--Memory-->
                        <div class="tab-pane fade in" id="memory">
                            <asp:DataList ID="memorylist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--Power Supply-->
                        <div class="tab-pane fade in" id="powersupply">
                            <asp:DataList ID="powersuplist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--Booster-->
                        <div class="tab-pane fade in" id="booster">
                            <asp:DataList ID="boosterlist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
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

    <!--
    <fieldset>
        <legend>Market</legend>

        <div style="background-color: #434343;">
            <div style="background-color: #ff0000; float: left; width: 15%; margin-left: 0.5%; height: auto;">
                <h1>Parts</h1>
                <p>
                    <input type="checkbox" name="cpu" value="CPU">CPU
                </p>
                <p>
                    <input type="checkbox" name="powersupply" value="Power Supply">Power supply
                </p>
                <p>
                    <input type="checkbox" name="graphics" value="Graphics">Graphics
                </p>
                <p>
                    <input type="checkbox" name="ram" value="RAM">RAM
                </p>
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
    -->
</asp:Content>
