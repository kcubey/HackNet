<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="UserInventory.aspx.cs" Inherits="HackNet.Admin.UserInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
    <div class="panel panel-default">
        <div class="panel-header">
            <asp:Label runat="server" Text="User Iventory Panel" Font-Size="Large" Style="padding: 2%;" ForeColor="White"></asp:Label>
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
        <div class="panel-body" style="color: black; background-color: black;">
            <asp:DataList runat="server" ID="UserInventoryList">

            </asp:DataList>
        </div>
        <div class="panel-footer" style="background-color: #262526;">
        </div>
    </div>

</asp:Content>
