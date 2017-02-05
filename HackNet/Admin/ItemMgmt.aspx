<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="ItemMgmt.aspx.cs" Inherits="HackNet.Admin.ItemMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
    <script type="text/javascript">
        function showEditItemModal() {
            $('#EditItemModal').modal('show');
        }
        function showDeleteItemModal() {
            $('#DeleteItemModal').modal('show');
        }
    </script>
    <!-- DeleteItem -->
    <div id="DeleteItemModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Confirm Delete Item" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                <div class="modal-body" style="color: black">
                    <p>Are you sure you want to <span style="color:red; font-size:1em;">PERMAMENTLY</span> delete this</p>
                    <br />
                    <br />
                    <asp:Label runat="server" ForeColor="Black" Font-Size="Larger" ID="ConfirmDeleteItemName"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <p><span style="color:red;">WARNING</span> : THIS <span style="text-decoration:underline; color:darkred;">CANNOT</span> BE UNDONE</p>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Yes" ID="DeleteItemBtn" OnClick="DeletePartsInfoBtn_Click" />
                    <asp:Button runat="server" CssClass="btn btn-default" Text="No" ID="Close" data-dismiss="modal" />
                </div>
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
                        Display="Dynamic"
                        ControlToValidate="EditItemName"
                        ID="RegularExpressionValidatorForEditItemName"
                        ValidationExpression="^[a-zA-Z0-9'.&+/\s]{1,400}$"
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only Special characters allowed are &+. Maximum 400 characters allowed." ValidationGroup="ItemUpdate">
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
                        Display="Dynamic"
                        ControlToValidate="EditItemDesc"
                        ID="RegularExpressionValidatorForEditItemDesc"
                        ValidationExpression="^[a-zA-Z0-9'-'\#\,\?\*\-\(\)\._\&\+\/\s]{0,10000}$"
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only Special characters allowed are '*#?()/&+-_. Maximum 10000 characters allowed." ValidationGroup="ItemUpdate">
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Item Price -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Price" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemPrice" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        Display="Dynamic"
                        ControlToValidate="EditItemPrice"
                        ID="RegularExpressionValidatorForEditItemPrice"
                        ValidationExpression="^[0-9]*$"
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only numeric values are allowed." ValidationGroup="ItemUpdate">
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Item Bonus -->
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Bonus" CssClass="col-xs-3 col-form-label" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemBonus" Width="280px"></asp:TextBox>
                    <asp:RegularExpressionValidator
                        Display="Dynamic"
                        ControlToValidate="EditItemBonus"
                        ID="RegularExpressionValidatorForEditItemBonus"
                        ValidationExpression="^[0-9]*$"
                        runat="server"
                        ForeColor="Red"
                        ErrorMessage="Only numeric values are allowed." ValidationGroup="ItemUpdate">
                    </asp:RegularExpressionValidator>
                </div>
                <!-- Buttons -->
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Update" ID="UpdatePartsInfoBtn" ValidationGroup="ItemUpdate" OnClick="UpdatePartsInfoBtn_Click" />
                    <asp:Button runat="server" CssClass="btn btndelete btn-default" Text="Delete" ID="DeletePartsInfoBtn" OnClick="ConfirmDeletePartsInfoBtn_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
                    <li><a href="#AllItemTab" data-toggle="tab">All Items</a></li>
                    <li><a href="#ProcessItemTab" data-toggle="tab">Processors</a></li>
                    <li><a href="#GraphicItemTab" data-toggle="tab">Graphics Cards</a></li>
                    <li><a href="#MemoryItemTab" data-toggle="tab">Memory</a></li>
                    <li><a href="#PowerItemTab" data-toggle="tab">Power Supply</a></li>
                    <li><a href="#CreateItemTab" data-toggle="tab">Add Item</a></li>
                </ul>
            </div>
            <div class="panel-body">
                <div class="tab-content">
                    <!-- All Items -->
                    <div class="tab-pane fade in active" id="AllItemTab" style="height: 660px; overflow-y: auto;">
                        <h2>All Items</h2>
                        <asp:DataList ID="AllItemList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1000px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px; width: 300px;">
                                    <asp:Label runat="server" ID="ItemNameLbl"
                                        Text='<%#Eval("ItemName") %>'
                                        ForeColor="White" Font-Size="Large"
                                        Height="50px" Width="300px" /><br />
                                    <asp:Image runat="server" ID="ItemImage"
                                        Height="200px" Width="200px"
                                        ImageUrl='<%#Eval("ItemPic") %>' /><br />
                                    <asp:LinkButton runat="server" ID="EditItemBtn" CssClass="btn btn-default" Text="Edit"
                                        CommandArgument='<%#Eval("ItemID") %>' OnCommand="EditItemBtn_Command" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- Processor -->
                    <div class="tab-pane fade" id="ProcessItemTab" style="height: 660px; overflow-y: auto;">
                        <h2>Item - Processor</h2>
                        <asp:DataList ID="ProcessItemList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1065px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px; width: 300px;">
                                    <asp:Label runat="server" ID="ItemNameLbl"
                                        Text='<%#Eval("ItemName") %>'
                                        ForeColor="White" Font-Size="Large"
                                        Height="50px" Width="300px" /><br />
                                    <asp:Image runat="server" ID="ItemImage"
                                        Height="200px" Width="200px"
                                        ImageUrl='<%#Eval("ItemPic") %>' /><br />
                                    <asp:LinkButton runat="server" ID="EditItemBtn" CssClass="btn btn-default" Text="Edit"
                                        CommandArgument='<%#Eval("ItemID") %>' OnCommand="EditItemBtn_Command" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- Graphics Card -->
                    <div class="tab-pane fade" id="GraphicItemTab" style="height: 660px; overflow-y: auto;">
                        <h2>Item - Graphic Card</h2>
                        <asp:DataList ID="GraphicItemList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1065px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px; width: 300px;">
                                    <asp:Label runat="server" ID="ItemNameLbl"
                                        Text='<%#Eval("ItemName") %>'
                                        ForeColor="White" Font-Size="Large"
                                        Height="50px" Width="300px" /><br />
                                    <asp:Image runat="server" ID="ItemImage"
                                        Height="200px" Width="200px"
                                        ImageUrl='<%#Eval("ItemPic") %>' /><br />
                                    <asp:LinkButton runat="server" ID="EditItemBtn" CssClass="btn btn-default" Text="Edit"
                                        CommandArgument='<%#Eval("ItemID") %>' OnCommand="EditItemBtn_Command" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- Memory -->
                    <div class="tab-pane fade" id="MemoryItemTab" style="height: 660px; overflow-y: auto;">
                        <h2>Item - Memory</h2>
                        <asp:DataList ID="MemoryItemList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1065px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px; width: 300px;">
                                    <asp:Label runat="server" ID="ItemNameLbl"
                                        Text='<%#Eval("ItemName") %>'
                                        ForeColor="White" Font-Size="Large"
                                        Height="50px" Width="300px" /><br />
                                    <asp:Image runat="server" ID="ItemImage"
                                        Height="200px" Width="200px"
                                        ImageUrl='<%#Eval("ItemPic") %>' /><br />
                                    <asp:LinkButton runat="server" ID="EditItemBtn" CssClass="btn btn-default" Text="Edit"
                                        CommandArgument='<%#Eval("ItemID") %>' OnCommand="EditItemBtn_Command" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- Power Supply -->
                    <div class="tab-pane fade" id="PowerItemTab" style="height: 660px; overflow-y: auto;">
                        <h2>Item - Power Supply</h2>
                        <asp:DataList ID="PowerItemList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="1065px">
                            <ItemTemplate>
                                <div style="margin: 4px; margin-bottom: 20px; width: 300px;">
                                    <asp:Label runat="server" ID="ItemNameLbl"
                                        Text='<%#Eval("ItemName") %>'
                                        ForeColor="White" Font-Size="Large"
                                        Height="50px" Width="300px" /><br />
                                    <asp:Image runat="server" ID="ItemImage"
                                        Height="200px" Width="200px"
                                        ImageUrl='<%#Eval("ItemPic") %>' /><br />
                                    <asp:LinkButton runat="server" ID="EditItemBtn" CssClass="btn btn-default" Text="Edit"
                                        CommandArgument='<%#Eval("ItemID") %>' OnCommand="EditItemBtn_Command" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <!-- Add Items -->
                    <div class="tab-pane fade" id="CreateItemTab">
                        <h2>Item Creator</h2>
                        <div class="container-fluid" style="color: black; background-color: gray;">
                            <br />
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Item Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="ItemName" ForeColor="black"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    Display="Dynamic"
                                    ControlToValidate="ItemName"
                                    ID="RegularExpressionValidatorForItemName"
                                    ValidationExpression="^[a-zA-Z0-9\-\s]{1,400}$"
                                    runat="server"
                                    ForeColor="Red"
                                    ErrorMessage="Only Special character allowed is -  Maximum 400 characters allowed." ValidationGroup="ItemUpdate">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Item Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:DropDownList runat="server" ID="ItemTypeList" ForeColor="black">
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
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Item Description: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="ItemDesc" ForeColor="black" TextMode="MultiLine" Height="100px" Width="196px"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    Display="Dynamic"
                                    ControlToValidate="ItemDesc"
                                    ID="RegularExpressionValidatorForItemDesc"
                                    ValidationExpression="^[a-zA-Z0-9'-'\#\,\?\*\-\(\)\._\&\+\/\s]{0,10000}$"
                                    runat="server"
                                    ForeColor="Red"
                                    ErrorMessage="Only Special characters allowed are '*#?()/&+-_. Maximum 10000 characters allowed." ValidationGroup="ItemUpdate">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Item Price: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="ItemPrice" ForeColor="black"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    Display="Dynamic"
                                    ControlToValidate="ItemPrice"
                                    ID="RegularExpressionValidatorForItemPrice"
                                    ValidationExpression="^[0-9]{0,3}$"
                                    runat="server"
                                    ForeColor="Red"
                                    ErrorMessage="Only numeric values are allowed. Maximum value of 999." ValidationGroup="ItemUpdate">
                                </asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Item Bonus: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="ItemStat" ForeColor="black"></asp:TextBox>
                                <asp:RegularExpressionValidator
                                    Display="Dynamic"
                                    ControlToValidate="ItemStat"
                                    ID="RegularExpressionValidatorForItemStat"
                                    ValidationExpression="^(1|2|3|4|5|6|7|8|9|10|11|12|13|14|15|16|17|18|19|20|21|22|23|24|25|26|27|28|29|30|31|32|33|34|35|36|37|38|39|40|41|42|43|44|45|46|47|48|49|50)$"
                                    runat="server"
                                    ForeColor="Red"
                                    ErrorMessage="Only numeric values are allowed. Minimum value of 1. Maximum value of 50." ValidationGroup="ItemUpdate">
                                </asp:RegularExpressionValidator>
                            </div>
                            <asp:Button runat="server" ID="btnAddItem" CssClass="btn btn-default" OnClick="btnAddItem_Click" Text="Add Item" />
                        </div>
                    </div>
                </div>

            </div>
           
        </div>
    </div>
</asp:Content>
