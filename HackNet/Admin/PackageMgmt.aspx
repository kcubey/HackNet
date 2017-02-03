<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="PackageMgmt.aspx.cs" Inherits="HackNet.Admin.PackageMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">

    <script>
        function showEditItemModal() {
            $('#EditItemModal').modal('show');
        }
    </script>

    
                            
<!-- =============== START MODAL CONTENT ============== -->
    <div id="EditItemModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Edit Item" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Name" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemName" Width="280px"></asp:TextBox>

                </div>
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Type" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemType" Enabled="false" Width="280px"></asp:TextBox>
                </div>
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Description" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemDesc" Width="280px"></asp:TextBox>
                </div>
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Price" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemPrice" Width="280px"></asp:TextBox>
                </div>
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Bonus" />
                    <asp:TextBox autocomplete="off" runat="server" ID="EditItemBonus" Width="280px"></asp:TextBox>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Update" ID="UpdatePartsInfoBtn" OnClick="UpdatePartsInfoBtn_Click" />
                </div>
            </div>
        </div>
    </div>
<!-- =============== END MODAL CONTENT ============== -->

    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
              <!--      <li class="active"><a href="#tab1default" data-toggle="tab">All</a></li> -->
                    <li class="active"><a href="#tab1default" data-toggle="tab">Add</a></li>
                    <li><a href="#tab2default" data-toggle="tab">Edit</a></li>
                </ul>
            </div>
            <div class="panel-body">
                <div class="tab-content">
<!-- ========== START TAB 1 CONTENT - ADD PACKAGE ========== -->                    
                    <div class="tab-pane fade in active" id="tab1default">
                        <h2>Add Packages</h2>
                        <div class="container-fluid" style="color: black; background-color: gray;">
                            <br />
                            <asp:UpdatePanel ID="AddPackagePanel" runat="server">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <asp:Label runat="server" Text="Item Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                        <asp:DropDownList runat="server" ID="itemTypeDDL" OnSelectedIndexChanged="DisplayItems" AutoPostBack="true">
                                            <asp:ListItem Value="-2">Choose An Item Type</asp:ListItem>
                                            <asp:ListItem Value="1">Processor</asp:ListItem>
                                            <asp:ListItem Value="4">Graphic Card</asp:ListItem>
                                            <asp:ListItem Value="2">Memory</asp:ListItem>
                                            <asp:ListItem Value="3">Power Supply</asp:ListItem>
                                            <asp:ListItem Value="0">Booster</asp:ListItem>
                                            <asp:ListItem Value="5">Currency</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group row">
                                        <asp:Label runat="server" Text="Item List: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                        <asp:DataList ID="SelectionDataList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                                            <ItemTemplate>
                                                <div style="margin: 10px;">
                                                    <asp:LinkButton runat="server" ID="itemName" OnCommand="SelectedItem_Command" 
                                                        CommandArgument='<%# Eval("ItemID")%>' forecolor="Black" Text='<%#Eval("ItemName") %>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <div class="form-group row">
                                        <asp:Label runat="server" Text="Item Selected: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                        <asp:Label runat="server" id="selectedItemLbl" forecolor="White" Text=""></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Package Description: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgDesc" TextMode="MultiLine"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Package Price ($): " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgPrice"></asp:TextBox>
                                <asp:RegularExpressionValidator 
                                    ID="priceValidator" runat="server" 
                                    ErrorMessage="* Enter up to 2 decimal places only" ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$"
                                    ControlToValidate="pkgQuantity" ForeColor="Red">
                               </asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Quantity: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgQuantity"></asp:TextBox>
                                <asp:RegularExpressionValidator 
                                    ID="quantityValidator" runat="server" 
                                    ErrorMessage="* Enter whole numbers only" ValidationExpression="^\d+$"
                                    ControlToValidate="pkgQuantity" ForeColor="Red">
                               </asp:RegularExpressionValidator>
                            </div>
                            <asp:Button runat="server" ID="addPackage" CssClass="btn btn-default" OnClick="btnAddPackage_Click" Text="Add Package" />
                            <br /><br />
                        </div>
                    </div>
<!-- ================= END TAB 1 CONTENT  ================== -->                    

<!-- ========== START TAB 2 CONTENT - EDIT PACKAGE ========== -->                    
                    <div class="tab-pane fade" id="tab2default">
                        <h2>Edit Package</h2>
                        Click on the package to edit.
                        <asp:Label runat="server" Text="Item List: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:DataList ID="EditPackage" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <div style="margin: 10px;">
                                    <asp:LinkButton runat="server" ID="itemName" OnCommand="EditPackage_Command" 
                                        CommandArgument='<%# Eval("PackageID")%>' forecolor="Black">
                                        <asp:Label ID="packageDesc" CssClass="redirectLbl" runat="server" Text="Description" Font-Size="Smaller"></asp:Label>
                                        <br />
                                        <asp:Label ID="packageName" CssClass="redirectLbl" runat="server" Text="Package "></asp:Label>
                                            <asp:Label ID="packageNo" CssClass="redirectLbl" runat="server" Text='<%#Eval("PackageId")%>'></asp:Label>
                                            <asp:Label ID="packagePrice" CssClass="redirectLbl" runat="server" Text=" - $"></asp:Label>
                                            <asp:Label ID="packageCost" CssClass="redirectLbl" runat="server" Text='<%#Eval("Price")%>'></asp:Label>
                                    </asp:LinkButton>
                                </div>
                             </ItemTemplate>
                         </asp:DataList>
                    </div>
<!-- ================= END TAB 2 CONTENT  ================== -->                                        
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
