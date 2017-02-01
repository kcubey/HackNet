<%@ Page Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Currency.aspx.cs" Inherits="HackNet.Game.Currency" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameContent" runat="server">

    <link rel="stylesheet" href="/Market1/market1.css" />
    <!-- this cssfile can be found in the jScrollPane package 
    <link rel="stylesheet" type="text/css" href="jquery.jscrollpane.css" />
    <!-- latest jQuery direct from google's CDN
    script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <!-- the jScrollPane script
    script type="text/javascript" src="jquery.jscrollpane.min.js"></script
-->
    <script>
        function showPopup() {
            $('#popupConfirmation').modal('show');
        }
    </script>
    

    <link rel="stylesheet" href="/payment/backend/redirectimagebutton.css" />

    <style>
        .customscrollbar{
        }

        .nav-tabs > li > a{
            color:limegreen;
        }
        .nav-tabs > li.active > a,
        .nav-tabs > li.active > a:hover,
        .nav-tabs > li.active > a:focus {
            background-color:rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
        }

        .nav > li a:hover {
            background-color: rgba(10, 10, 10, 0.9);
            color: white;
            border: 0px solid #333;
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
                        Enter the number of bucks you wish to convert to coins. Click on 'Convert' once you are done.
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
                            data-target="#popupConfirmation" runat="server" type="button" Text="OPen Modal"/>
                        
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
                            <br />
                            <asp:Label ID="packageBuckQuantity" CssClass="redirectLbl" runat="server" Text="100" Font-Size="Smaller"></asp:Label>
                            <asp:Label ID="packageItem" CssClass="redirectLbl" runat="server" Text="Bucks" Font-Size="Smaller"></asp:Label>
                            <br />
                            <asp:Label ID="packageName" CssClass="redirectLbl" runat="server" Text="Package "></asp:Label>
                            <asp:Label ID="packageNo" CssClass="redirectLbl" runat="server" Text="1"></asp:Label>
                            <asp:Label ID="packagePrice" CssClass="redirectLbl" runat="server" Text=" - $"></asp:Label>
                            <asp:Label ID="packageCost" CssClass="redirectLbl" runat="server" Text="1000"></asp:Label>
                        </asp:LinkButton>

<!-- REpeater
                        <asp:DataList ID="packageDL" runat="server" RepeatColumns="3" RepeatLayout="Table" Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="itemName" runat="server" Text='< % #Eval("ItemName") %>' ForeColor="White" Font-Size="Large"></asp:Label>
                                <br />
                                <asp:Image ID="itemImg" runat="server"
                                    Width="200px" Height="200px"
                                    ImageUrl='< % #Eval("PackagePic")%>' />
                                <br />
                                <asp:LinkButton runat="server" ID="ViewMore" CommandArgument='< % #Eval("ItemNo") %>' OnCommand="ViewMore_Command" Text="View more"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:DataList>

                        
                        <asp:Repeater ID="packageRepeater" runat="server">
                            <ItemTemplate>
                                <asp:LinkButton ID="packageButton" class="redirectButton" runat="server" onclick="buyPackage_Click" Font-Underline="False">
                                    <asp:Image ID="packageImage" runat="server" CssClass="redirectImg" ImageUrl='< % #Eval("PackagePic")%>'  BackColor="Transparent" />
                                    <br />
                                    <asp:Label ID="packageBuckQuantity" CssClass="redirectLbl" runat="server" Text='< % # Eval("Quantity")%>' Font-Size="Smaller"></asp:Label>
                                    <asp:Label ID="packageDesc" CssClass="redirectLbl" runat="server" Text='< % # Eval("PackageDesc")%>' Font-Size="Smaller"></asp:Label>
                                    <br />
                                    <asp:Label ID="packageName" CssClass="redirectLbl" runat="server" Text="Package "></asp:Label>
                                        <asp:Label ID="packageNo" CssClass="redirectLbl" runat="server" Text='< % # Eval("PackageNo")%>'></asp:Label>
                                        <asp:Label ID="packagePrice" CssClass="redirectLbl" runat="server" Text=" - $"></asp:Label>
                                        <asp:Label ID="packageCost" CssClass="redirectLbl" runat="server" Text='< % # Eval("PackagePrice")%>'></asp:Label>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                        -->
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
        <h2>Add New Buck Packages</h2>
        <div class="form-group row">
            <asp:Label runat="server" Text="Package Image: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:FileUpload ID="UploadPhoto" runat="server" />
            <asp:Image ID="imgViewFile" runat="server" />
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Package Price: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="packPrice"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Buck Quantity: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="buckQuantity"></asp:TextBox>
        </div>
        <!-- <asp:Button  ID="btnAddItem" CssClass="btn btn-default" OnClick=btnAddBuckPackage_Click" Text="Add Buck Package" />-->
        <asp:Button runat="server" ID="btnAddPkg" CssClass="btn btn-default" Text="Add Buck Package" />
    </div>

    
</asp:Content>
