﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="UserInventory.aspx.cs" Inherits="HackNet.Admin.UserInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
    <script type="text/javascript">
        function ShowPopUp() {
            $('#UserInvenModal').modal('show');
        }
    </script>
    <div id="UserInvenModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" ID="OutComeLbl" ForeColor="Black" Font-Size="Larger" Font-Bold="true" />
                </div>
                <div class="modal-body">
                    <asp:Label runat="server" ID="WarningLbl" Text="*WARNING*" ForeColor="Red" Font-Size="X-Large" /><br />
                   <asp:Label runat="server" ID="WarningMessageLbl" ForeColor="Black" />
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-default">
        <div class="panel-header">
            <asp:Label runat="server" Text="User Inventory Panel" Font-Size="Large" Style="padding: 2%;" ForeColor="White"></asp:Label>
        </div>
        <div class="panel-body" style="color: black; background-color: black;">
            <asp:GridView runat="server" ForeColor="White" BorderStyle="None" CssClass="table" ID="AdminUsersView" OnLoad="AdminUsersView_Load">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="btn btn-default" Text="View" ID="ViewUserInventory" OnCommand="ViewUserInventory_Command" CommandArgument='<%#Bind("UserID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="panel-header">
            <asp:Label runat="server" ID="UserInvLbl" Font-Size="Large" Style="padding: 2%;" ForeColor="White"></asp:Label>
        </div>
        <div class="panel-body" style="color: black; background-color: black;">
            <asp:GridView runat="server" ForeColor="White" BorderStyle="None" CssClass="table" ID="UserInvView">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="btn btn-default" Text="Delete" ID="DeleteItem" OnCommand="DeleteItem_Command" CommandArgument='<%#Bind("ItemID") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="panel-footer" style="background-color: #262526;">
            <h2>Add Item to user inventory</h2>
            <br />
            <div class="form-group row">
                <asp:Label runat="server" Text="UserID: " CssClass="col-xs-3 col-form-label" />
                <asp:TextBox runat="server" ID="UserIDTxtbox" Width="280px" ForeColor="Black"></asp:TextBox>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" Text="Items: " CssClass="col-xs-3 col-form-label" />
                <asp:DropDownList runat="server" ID="AllItemsList" ForeColor="Black">
                </asp:DropDownList>
            </div>
            <div class="form-group row">
                <asp:Label runat="server" Text="Quantity: " CssClass="col-xs-3 col-form-label" />
                <asp:TextBox runat="server" ID="ItemQuantityTxtbox" Width="280px" ForeColor="Black" />
                <asp:RegularExpressionValidator runat="server" ID="QuantityValidator"
                     ForeColor="Red"
                     ErrorMessage="Please Enter Only Numbers"
                     ControlToValidate="ItemQuantityTxtbox"
                     ValidationExpression="^\d+$" />
            </div>
            <br />
                <asp:Button runat="server" ID="AddItemToUserInv" OnClick="AddItemToUserInv_Click" CssClass="btn btn-default" Text="Add Item" />
        </div>
    </div>

</asp:Content>
