<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="ItemMgmt.aspx.cs" Inherits="HackNet.Admin.ItemMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">

    <script>
        function showEditItemModal() {
            $('#EditItemModal').modal('show');
        }
        function showDeleteItemModal() {
            $('#DeleteItemModal').modal('show');
        }
    </script>
    <!-- DeleteItem -->
    <div id="DeleteItemModal" class="modal fade" role="dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <asp:Label runat="server" Text="Confirm Delete Item" ForeColor="Black" Font-Size="Larger"></asp:Label>
            </div>
            <div class="modal-body" style="color: black">
                <p>Are you sure you want to permamently delete this</p>
                <asp:Label runat="server" ForeColor="Black" Font-Size="Larger" ID="ConfirmDeleteItemName"></asp:Label>
            </div>
            <div class="modal-footer">
                <asp:Button runat="server" CssClass="btn btn-default" Text="Yes" ID="DeleteItemBtn" OnClick="DeletePartsInfoBtn_Click" />
                <asp:Button runat="server" CssClass="btn btn-default" Text="No" ID="Close"  data-dismiss="modal"  />
            </div>
        </div>
    </div>
    <!-- EditItem -->
    <div id="EditItemModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Edit Item" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                <!-- Item Name -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Name" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemName" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        Display = "Dynamic" 
                        ControlToValidate = "EditItemName" 
                        ID="RegularExpressionValidatorForEditItemName" 
                        ValidationExpression = "^[a-zA-Z0-9'.&+/\s]{1,400}$" 
                        runat="server" 
                        ForeColor="Red"
                        ErrorMessage="Maximum 400 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Item Type -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Type" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemType" Enabled="false" Width="280px"></asp:TextBox>
                </div>
                <!-- Item Desc -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Description" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemDesc" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        Display = "Dynamic" 
                        ControlToValidate = "EditItemDesc" 
                        ID="RegularExpressionValidatorForItemDesc" 
                        ValidationExpression = "^[a-zA-Z0-9'-'._%&+/\s]{0,5000}$" 
                        runat="server" 
                        ForeColor="Red"
                        ErrorMessage="Maximum 5000 characters allowed.">
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Item Price -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Price" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemPrice" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        Display = "Dynamic" 
                        ControlToValidate="EditItemPrice"
                        ID="RegularExpressionValidatorForItemPrice" 
                        ValidationExpression="^[0-9]*$" 
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only numeric allowed." >
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Item Bonus -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Bonus" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemBonus" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator 
                        Display = "Dynamic" 
                        ControlToValidate="EditItemBonus"
                        ID="RegularExpressionValidatorForItemBonus" 
                        ValidationExpression="^[0-9]*$" 
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only numeric allowed." >
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Buttons -->
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Update" ID="UpdatePartsInfoBtn" OnClick="UpdatePartsInfoBtn_Click" />
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Delete" ID="DeletePartsInfoBtn" OnClick="ConfirmDeletePartsInfoBtn_Click" />
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
              <!--      <li class="active"><a href="#tab1default" data-toggle="tab">All</a></li> -->
                    <li><a href="#tab1default" data-toggle="tab">All Items</a></li>
                    <li><a href="#tab2default" data-toggle="tab">Packages</a></li>
                    <li><a href="#tab3default" data-toggle="tab">Processors</a></li>
                    <li><a href="#tab4default" data-toggle="tab">Graphics Cards</a></li>
                    <li><a href="#tab5default" data-toggle="tab">Memory</a></li>
                    <li><a href="#tab6default" data-toggle="tab">Power Supply</a></li>
                    <li><a href="#tab7default" data-toggle="tab">Add Items</a></li>
                </ul>
            </div>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tab1default">
                        <h2>All Items</h2>

                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px;">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server" 
                                        Width="200px" Height="200px"     
                                        ImageUrl='<%#Eval("ItemPic")%>'/>
                                    <br />
                                    <asp:LinkButton runat="server" ID="EditItemBTn" OnCommand="EditItemBTn_Command" CommandArgument='<%# Eval("ItemID")%>' Text="Edit" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="tab-pane fade" id="tab2default">
                        <h2>Packages</h2>

                    </div>

                    <!--Processor-->
                    <div class="tab-pane fade" id="tab3default">
                        <h2>Processors</h2>
                        <asp:DataList ID="ProcessList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px;">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server" 
                                        Width="200px" Height="200px"     
                                        ImageUrl='<%#Eval("ItemPic")%>'/>
                                    <br />
                                    <asp:LinkButton runat="server" ID="EditItemBTn" OnCommand="EditItemBTn_Command" CommandArgument='<%# Eval("ItemID")%>' Text="Edit" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <!--Graphics-->
                    <div class="tab-pane fade" id="tab4default">
                        <h2>Graphics Cards</h2>
                        <asp:DataList ID="GPUList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px;">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server" 
                                        Width="200px" Height="200px"     
                                        ImageUrl='<%#Eval("ItemPic")%>'/>
                                    <br />
                                    <asp:LinkButton runat="server" ID="EditItemBTn" OnCommand="EditItemBTn_Command" CommandArgument='<%# Eval("ItemID")%>' Text="Edit" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <!--Memory-->
                    <div class="tab-pane fade" id="tab5default">
                        <h2>Memory</h2>
                        <asp:DataList ID="MemoryList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px;">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server" 
                                        Width="200px" Height="200px"     
                                        ImageUrl='<%#Eval("ItemPic")%>'/>
                                    <br />
                                    <asp:LinkButton runat="server" ID="EditItemBTn" OnCommand="EditItemBTn_Command" CommandArgument='<%# Eval("ItemID")%>' Text="Edit" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <!--Power Supply-->
                    <div class="tab-pane fade" id="tab6default">
                        <h2>Power Supply</h2>
                        <asp:DataList ID="PowerSupList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px;">
                                    <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large" Height="50px" Width="196px"></asp:Label>
                                    <br />
                                    <asp:Image ID="itemImg" runat="server" 
                                        Width="200px" Height="200px"     
                                        ImageUrl='<%#Eval("ItemPic")%>'/>
                                    <br />
                                    <asp:LinkButton runat="server" ID="EditItemBTn" OnCommand="EditItemBTn_Command" CommandArgument='<%# Eval("ItemID")%>' Text="Edit" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <!--Add Item-->
                    <div class="tab-pane fade" id="tab7default">
                        <h2>Add Item</h2>
                        <div class="container-fluid" style="color: black; background-color: gray;">
                            <br />
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
                                <asp:TextBox runat="server" ID="ItemDesc" TextMode="MultiLine" Height="50px" Width="196px"></asp:TextBox>
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
                            <br />
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    

</asp:Content>
