<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="HackNet.Game.Currency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />
    <!-- this cssfile can be found in the jScrollPane package -->
    <link rel="stylesheet" type="text/css" href="jquery.jscrollpane.css" />
    <!-- latest jQuery direct from google's CDN -->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <!-- the jScrollPane script -->
    <script type="text/javascript" src="jquery.jscrollpane.min.js"></script>

    <script>
        function showPopup() {
            $('#popupConfirmation').modal('show');
        }
    </script>
    

    <link rel="stylesheet" href="/payment/backend/redirectimagebutton.css" />

    <style>
        .customscrollbar{

        }
    </style>

    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Market - Currency</h3>
		</div>
		<div class="panel-body">
            <div class="col-xs-3">
                <!-- required for floating -->
                <!-- Nav tabs -->
                <ul class="nav nav-tabs tabs-left fade in active" style="width:20%;">
                    <li class="active"><a href="#conversion" data-toggle="tab">Conversion</a></li>
                    <li><a href="#packages" data-toggle="tab">Packages</a></li>
                    <li><a href="#memory" data-toggle="tab">Memory</a></li>
                </ul>
            </div>
            <div class="col-xs-9" style="float:left ;">
                <!-- Tab panes -->
                <div class="tab-content">

                    <!--Conversion-->
                    <div class="tab-pane fade in active" id="conversion">
                        Enter the number of bucks you wish to convert to coins.
                        <hr />
                        <asp:UpdatePanel ID="ConversionPanel" runat="server">
                            <ContentTemplate>
                                Bucks:
                                <asp:TextBox ID="buckTextBox" CssClass="form-control" runat="server" OnTextChanged="buckTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
                                <asp:RangeValidator 
                                    ID="buckValidator" ControlToValidate="buckTextBox" 
                                    MinimumValue="0" Type="Integer" runat="server"
                                    ErrorMessage="* Please enter a valid number" ForeColor="Red">
                                </asp:RangeValidator>
                               <br />
                               <asp:RegularExpressionValidator 
                                    ID="buckExValidator" runat="server" 
                                    ErrorMessage="* Please use whole numbers only" ValidationExpression="^\d+$"
                                    ControlToValidate="buckTextBox" ForeColor="Red">
                               </asp:RegularExpressionValidator>
                                    <!-- expressions: ^[1-9]\d$ || ^\d+$ || ^[0-9]*$ but allows space-->

                               <br />Coins: <asp:Label ID="convertedCoinLabel" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br /><br />

                        <asp:Button ID="ConversionButton" runat="server" CssClass="btn btn-success" Text="Convert" OnClick="ConversionButton_Click"/>
<!-- ============================= -->

                        <asp:Button ID="openButton" class="btn btn-info btn-lg" data-toggle="modal" 
                            data-target="#popupConfirmation" runat="server" Text="OPen Modal"/>
                        
                        <div id="popupConfirmation" class="modal fade" role="dialog">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        <asp:Label ID="headerconvert" runat="server" Text="*WARNING*" ForeColor="Blue" Font-Size="Large"></asp:Label>
                                    </div>
                                    <div class="modal-body" style="color: black;">
                                        <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button CssClass="btn btn-default" runat="server" OnClick="mcButton_Click" Text="Continue" />
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancel</button>
                                    </div>
                                </div>

                            </div>
                        </div>

                        <!-- KTODO: change alert to modal-->

                    </div>

                    <!--Packages-->
                    <div class="tab-pane fade in" id="packages">
                        <asp:Label ID="warning" runat="server" Text="* WARNING *" ForeColor="Red" Font-Bold="True" Font-Size="Large"></asp:Label>
                        <br />These premium packages require the use of REAL money.
                        <hr />
                        <asp:LinkButton ID="packageButton" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False">
                            <asp:Image ID="packageImage" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                            <br /><br />
                            <asp:Label ID="packageName" CssClass="redirectLbl" runat="server" Text="Package "></asp:Label>
                            <asp:Label ID="packageNo" CssClass="redirectLbl" runat="server" Text="1"></asp:Label>
                            <asp:Label ID="packagePrice" CssClass="redirectLbl" runat="server" Text=" - $"></asp:Label>
                            <asp:Label ID="packageCost" CssClass="redirectLbl" runat="server" Text="1000"></asp:Label>
                        </asp:LinkButton>
                        
                        <!-- KTODO: make packages dynamic -->

                        <asp:LinkButton ID="LinkButton2" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False">
                            <asp:Image ID="Image2" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                            <br /><br />
                            <asp:Label ID="Label2" CssClass="redirectLbl" runat="server" Text="Package 2 - SGD$100"></asp:Label>
                        </asp:LinkButton>

                        <asp:LinkButton ID="LinkButton3" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False">
                            <asp:Image ID="Image3" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                            <br /><br />
                            <asp:Label ID="Label3" CssClass="redirectLbl" runat="server" Text="Package 3 - SGD$50"></asp:Label>
                        </asp:LinkButton>

                        <asp:LinkButton ID="LinkButton4" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False"> 
                            <asp:Image ID="Image4" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                            <br /><br />
                            <asp:Label ID="Label4" CssClass="redirectLbl" runat="server" Text="Package 4 - SGD$20"></asp:Label>
                        </asp:LinkButton>

                        <asp:LinkButton ID="LinkButton5" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False"> 
                            <asp:Image ID="Image5" runat="server" CssClass="redirectImg" ImageUrl="/payment/backend/package-2.png"  BackColor="Transparent" />
                            <br /><br />
                            <asp:Label ID="Label5" CssClass="redirectLbl" runat="server" Text="Package 5 - SGD$350"></asp:Label>
                        </asp:LinkButton>
                    </div>

                     <!--Memory-->
                    <div class="tab-pane fade in" id="memory">
                        <asp:DataList ID="memorylist" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='<%#Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='<%#Eval("ItemPic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='<%#Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <br /><br /><br />

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






    <fieldset>
        <legend>Market</legend>

        <div style="background-color:#434343;">
            <div style="background-color:#ff0000; float:left; width:15%; margin-left:0.5%; height:auto;">
                <h1>Currency</h1>
                    <p><input type="checkbox" name="coins" value="Coins">Coins</p>
                    <p><input type="checkbox" name="bytedollar" value="ByteDollar">ByteDollar</p>
            </div>
            <div class="customscrollbar" style="background-color:#4cff00; float:left; width:82%; padding-left:1%; margin-left:1%; margin-right:0.5%; height:400px; overflow:scroll; overflow-x:hidden;">
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ff6a00;">randombox left</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#bf4646;">randombox mid</div>
                <div style="float:left; width:31%; height:230px; margin:1%; background-color:#ffd800;">randombox right</div>
                <div style="clear:both;"></div>
            </div>
            <div style="clear:both;"></div>
        </div>
    </fieldset>

</asp:Content>
