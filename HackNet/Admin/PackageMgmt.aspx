<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="PackageMgmt.aspx.cs" Inherits="HackNet.Admin.PackageMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">

    <script>
        function showEditModal() {
            $('#EditModal').modal('show');
        }
    </script>
    <link rel="stylesheet" href="/payment/backend/redirectimagebutton.css" />

    
                            
<!-- =============== START MODAL CONTENT ============== -->
    <div id="EditModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Edit Package" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Package ID: " CssClass="col-xs-3 col-form-label"/>
                    <asp:TextBox autocomplete="false" runat="server" ID="EditPackageId" Enabled="false" Width="280px" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Package Description: " CssClass="col-xs-3 col-form-label"/>
                    <asp:TextBox autocomplete="false" runat="server" ID="EditPackageDesc" TextMode="MultiLine" Width="280px" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Package Price: " CssClass="col-xs-3 col-form-label"/>
                    <asp:TextBox autocomplete="false" runat="server" ID="EditPackagePrice" Width="280px" CssClass="form-control"></asp:TextBox>
                </div>

                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Package Item: " CssClass="col-xs-3 col-form-label"/>
                    <asp:TextBox autocomplete="false" runat="server" ID="EditItem" Enabled="false" Width="280px" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="modal-body" style="color: black">
                    <asp:Label runat="server" Text="Item Quantity: " CssClass="col-xs-3 col-form-label"/>
                    <asp:TextBox autocomplete="false" runat="server" ID="EditItemQuantity" Width="280px" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="form-group row">
                        <div class="col-xs-9">
                            <asp:RegularExpressionValidator ID="EditPackagePriceValidator" runat="server" 
                                ForeColor="Red"
                                ErrorMessage="Please enter a decimal number"
                                ControlToValidate="EditPackagePrice"
                                ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$"/><br />
                            <asp:RegularExpressionValidator ID="EditItemQuantityValidator" runat="server" 
                                ForeColor="Red"
                                ErrorMessage="Please enter whole numbers only"
                                ControlToValidate="EditItemQuantity"
                                ValidationExpression="^\d+$" />
                        </div>
                    </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Update" ID="UpdatePackageBtn" OnClick="UpdatePackageBtn_Click" />
                    <asp:Button runat="server" CssClass="btn btn-default" Text="Delete" ID="DeletePackageBtn" OnClick="DeletePackageBtn_Click" />
                </div>
            </div>
        </div>
    </div>
<!-- =============== END MODAL CONTENT ============== -->

    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
                    <li class="active"><a href="#tab1default" data-toggle="tab">Add Package</a></li>
                    <li><a href="#tab2default" data-toggle="tab">Edit Package</a></li>
                    <li><a href="#tab3default" data-toggle="tab">View User Currency</a></li>
                    <li><a href="#tab4default" data-toggle="tab">Add/Remove Currency</a></li>
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
                                        <asp:DropDownList runat="server" ID="itemTypeDDL" Cssclass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="DisplayItems" AutoPostBack="true">
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
                                        <asp:Label runat="server" id="itemSelectLbl" Text="Item Selected: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                        <asp:Label runat="server" id="selectedItemLbl" forecolor="White" Text=""></asp:Label>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="form-group row">
                                <asp:Label runat="server" id="descLbl" Text="Package Description: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgDesc" AutoComplete="false" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" id="priceLbl" Text="Package Price ($): " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgPrice" AutoComplete="false" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator 
                                    ID="priceValidator" runat="server" 
                                    ErrorMessage="* Enter up to 2 decimal places only" ValidationExpression="^[0-9]{0,6}(\.[0-9]{1,2})?$"
                                    ControlToValidate="pkgPrice" ForeColor="Red">
                               </asp:RegularExpressionValidator>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" id="qtyLbl" Text="Quantity: " CssClass="col-xs-3 col-form-label"></asp:Label>
                                <asp:TextBox runat="server" ID="pkgQuantity" AutoComplete="false" CssClass="form-control"></asp:TextBox>
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
                        <br />
                        <asp:Repeater ID="packageRepeater" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="itemName" class="redirectButton" runat="server" 
                                        OnCommand="EditPackage_Command"  CommandArgument='<%#Eval("PackageId")%>' Font-Underline="False">
                                    <asp:Image ID="packageImage" runat="server" CssClass="redirectImg" ImageUrl='<%#Eval("ItemPic")%>'  BackColor="Transparent" />
                                    <br />
                                    <asp:Label ID="packageDesc" CssClass="redirectLbl" runat="server" Text='<%#Eval("Description")%>' Font-Size="Smaller"></asp:Label>
                                    <br />
                                    <asp:Label ID="packageName" CssClass="redirectLbl" runat="server" Text="Package "></asp:Label>
                                        <asp:Label ID="packageNo" CssClass="redirectLbl" runat="server" Text='<%#Eval("PackageId")%>'></asp:Label>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
<!-- ================= END TAB 2 CONTENT  ================== -->    

<!-- ========== START TAB 3 CONTENT - VIEW USER Currency ========== -->                    
                    <div class="tab-pane fade" id="tab3default">
                        <h2>View Users' Currency</h2>
                        <br />
                        <asp:GridView runat="server" ForeColor="White" BorderStyle="None" CssClass="table" ID="ViewUserCurr" OnLoad="ViewUserCurr_Load">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
<!-- ================= END TAB 3 CONTENT  ================== -->             

<!-- ========== START TAB 4 CONTENT - Add/Remove Currency ========== -->                    
                    <div class="tab-pane fade" id="tab4default">
                        <h2>Add/Remove Currency</h2>
                        <div class="container-fluid" style="color: black; background-color: gray;">
                            <br />
                            <div class="form-group row">
                                <asp:Label runat="server" Text="UserID: " CssClass="col-xs-3 col-form-label" />
                                <asp:TextBox runat="server" ID="UserIDTxtbox" autocomplete="false" CssClass="form-control" Width="280px" ForeColor="Black"></asp:TextBox>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Items: " CssClass="col-xs-3 col-form-label" />
                                <asp:DropDownList runat="server" ID="userCurrencyDDL" Cssclass="btn btn-default dropdown-toggle" OnSelectedIndexChanged="DisplayItems" AutoPostBack="true">
                                            <asp:ListItem Value="1">Buck</asp:ListItem>
                                            <asp:ListItem Value="2">Coin</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                            <div class="form-group row">
                                <asp:Label runat="server" Text="Quantity: " CssClass="col-xs-3 col-form-label" />
                                <asp:TextBox runat="server" ID="userQuantityTxtbox" Autocomplete="false" CssClass="form-control" Width="280px" ForeColor="Black" />
                                <asp:RegularExpressionValidator runat="server" ID="userQuantityValidator"
                                     ForeColor="Red"
                                     ErrorMessage="Please enter a whole number"
                                     ControlToValidate="userQuantityTxtbox"
                                     ValidationExpression="^\d+$" />
                            </div>
                            <br />
                            <asp:Button runat="server" ID="AddUserCurrency" OnClick="AddUserCurrency_Click" CssClass="btn btn-default" Text="Add Currency" />
                            <asp:Button runat="server" ID="RemoveUserCurrency" OnClick="RemoveUserCurrency_Click" CssClass="btn btn-default" Text="Remove Currency" />
                            <br /><br />
                        </div>
                    </div>
<!-- ================= END TAB 4 CONTENT  ================== -->    
                </div>
            </div>
        </div>
    </div>
    
</asp:Content>
