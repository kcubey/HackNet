<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="ItemMgmt.aspx.cs" Inherits="HackNet.Admin.ItemMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">

    <div class="container-fluid">
        <div class="panel with-nav-tabs panel-default">
            <div class="panel-heading">
                <ul class="nav nav-tabs">
              <!--      <li class="active"><a href="#tab1default" data-toggle="tab">All</a></li> -->
                    <li><a href="#tab1default" data-toggle="tab">Items</a></li>
                    <li><a href="#tab2default" data-toggle="tab">Packages</a></li>
                    <li><a href="#tab3default" data-toggle="tab">Tab3</a></li>
                    <li><a href="#tab4default" data-toggle="tab">Tab4</a></li>
                </ul>
            </div>
            <div class="panel-body">
                <div class="tab-content">
                    <div class="tab-pane fade in active" id="tab1default">
                        <h2>Items</h2>

                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server" 
                                    Width="200px" Height="200px"     
                                    ImageUrl='<%#Eval("ItemPic")%>'/>
                            </ItemTemplate>
                        </asp:DataList>

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
                    </div>

                    <div class="tab-pane fade" id="tab2default">
                        <h2>Packages</h2>

                    </div>

                    <div class="tab-pane fade" id="tab3default">
                        <asp:DataList ID="GPUList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server" 
                                    Width="200px" Height="200px"     
                                    ImageUrl='<%#Eval("ItemPic")%>'/>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                    <div class="tab-pane fade" id="tab4default">
                        <asp:DataList ID="ProcessList" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server" 
                                    Width="200px" Height="200px"     
                                    ImageUrl='<%#Eval("ItemPic")%>'/>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
    </div>
    

</asp:Content>
