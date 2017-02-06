<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Parts.aspx.cs" Inherits="HackNet.Game.Parts" %>

<asp:Content ID="HeadCt" ContentPlaceHolderID="GameHeadPH" runat="server">
    <style>
        .nav-tabs > li > a {
            color: limegreen;
        }

        .nav-tabs > li.active > a,
        .nav-tabs > li.active > a:hover,
        .nav-tabs > li.active > a:focus {
            background-color: rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
        }

        .nav > li a:hover {
            background-color: rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
        }

        .viewMoreButton {
            color: #B6C5BE;
        }

            .viewMoreButton:hover {
                color: #fff;
                text-decoration: none;
            }

            .viewMoreButton:focus {
                color: #fff;
                text-decoration: none;
            }

            .viewMoreButton:visited {
                color: #fff;
                text-decoration: none;
            }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <script type="text/javascript">
        function showSellItemModal() {
            $('#SellItemModal').modal('show');
        }
    </script>

    <!-- SellItem -->
    <div id="SellItemModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Confirm Sell Item" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                <div class="modal-body" style="color: black">
                    <p>Are you sure you want to sell this</p>
                    <div class="form-group row">
                        <label class="col-xs-3 col-form-label">Item Name: </label>
                        <asp:Label runat="server" ForeColor="Black" Font-Size="Larger" ID="ConfirmSellItemName"></asp:Label>
                    </div>
                    <div class="form-group row">
                        <label class="col-xs-3 col-form-label">Item to sell for: </label>
                        <asp:Label runat="server" ForeColor="Black" Font-Size="Larger" ID="ConfirmSellItemPrice"></asp:Label>
                    </div>
                    <p><span style="color: red;">WARNING</span> : THIS <span style="text-decoration: underline; color: darkred;">CANNOT</span> BE UNDONE</p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Yes" ID="CfmSellBtn" OnClick="CfmSellBtn_Click" />
                    <asp:Button runat="server" CssClass="btn btn-default" Text="No" ID="Close" data-dismiss="modal" />
                </div>
            </div>
        </div>
    </div>

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
                        <li><a href="#useritems" data-toggle="tab">Your Items</a></li>
                    </ul>
                </div>
                <div class="col-xs-9" style="float: left; height: 900px; overflow: scroll; overflow-x: hidden;">
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
                                        <asp:LinkButton CssClass="viewMoreButton" runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
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
                                        <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemId") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                        <!--User Items-->
                        <div class="tab-pane fade in" id="useritems">
                            <asp:DataList ID="UserInvList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                <ItemTemplate>
                                    <div style="margin: 3px;">
                                        <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px"></asp:Label>
                                        <br />
                                        <asp:Image ID="itemImg" runat="server"
                                            Width="200px" Height="200px"
                                            ImageUrl='<%#Eval("ItemPic")%>' />
                                        <br />
                                        <asp:LinkButton runat="server" ID="SellItem" CommandArgument='<%#Eval("ItemId") %>' OnCommand="SellItem_Command" Text="Sell Item"></asp:LinkButton>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
