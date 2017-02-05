<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Inventory.aspx.cs" Inherits="HackNet.Game.Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">
    <script type="text/javascript">
        function showItemPopUp() {
            $('#ItemViewModel').modal('show');
        }
    </script>
    <div id="ItemViewModel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ForeColor="Black" Font-Size="Larger" Text="Item Information"></asp:Label>
                </div>
                <div class="modal-body">
                    <asp:Table runat="server" ForeColor="Black" CssClass="table">
                        <asp:TableRow>
                            <asp:TableCell Width="100px">
                                 <asp:Label runat="server" Text="Item Name: " />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="ItemNameLbl" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="100px">
                                 <asp:Label runat="server" Text="Item Type: " />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="ItemTypeLbl" />
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell Width="110px">
                                 <asp:Label runat="server" Text="Item Description: " />
                            </asp:TableCell>
                            <asp:TableCell>
                                <asp:Label runat="server" ID="ItemDescLbl" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <a href="Parts.aspx">Wanna sell or get more parts? Head over to our Market!</a>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

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
                        <asp:DataList ID="AllPartList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <div style="width: 300px; margin: 0 auto;">
                                    <asp:Label ID="itemName" runat="server" Height="50px" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server"
                                        Width="200px" Height="200px"
                                        ImageUrl='<%#Eval("ItemPic")%>' />
                                    <br />
                                    <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />
                                </div>
                                <hr />
                            </ItemTemplate>

                        </asp:DataList>

                    </div>
                    <div class="tab-pane fade" id="tab2default">
                        <asp:DataList ID="ProcessList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <div style="width: 300px">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server"
                                        Width="200px" Height="200px"
                                        ImageUrl='<%#Eval("ItemPic")%>' />
                                </div>
                                <br />
                                <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="tab-pane fade" id="tab3default">
                        <asp:DataList ID="GPUList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='<%#Eval("ItemPic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="tab-pane fade" id="tab4default">
                        <asp:DataList ID="MemoryList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <div style="width: 300px">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server"
                                        Width="200px" Height="200px"
                                        ImageUrl='<%#Eval("ItemPic")%>' />
                                </div>
                                <br />
                                <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="tab-pane fade" id="tab5default">
                        <asp:DataList ID="PowerSupList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <div style="width: 300px">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server"
                                        Width="200px" Height="200px"
                                        ImageUrl='<%#Eval("ItemPic")%>' />
                                </div>
                                <br />
                                <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="tab-pane fade" id="tab6default">
                        <asp:DataList ID="BonusList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px" Style="margin: 0 auto;">
                            <ItemTemplate>
                                <div style="width: 300px">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server"
                                        Width="200px" Height="200px"
                                        ImageUrl='<%#Eval("ItemPic")%>' />
                                </div>
                                <br />
                                <asp:LinkButton runat="server" ID="ViewItem" CssClass="btn btn-default" OnCommand="ViewItem_Command" CommandArgument='<%#Eval("ItemId") %>' Text="View" />

                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </div>
   
</asp:Content>
