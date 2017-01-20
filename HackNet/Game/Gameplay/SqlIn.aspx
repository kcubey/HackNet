<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SqlIn.aspx.cs" Inherits="HackNet.Game.Gameplay.SqlIn" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">

    </script>
    <div onkeypress="return CancelReturnKey();">
        <div class="row" style="border: 1px solid black; margin-left: 0; margin-right: 0;">
            <div class="col-xs-12 col-sm-6 col-md-8" style="border: 1px solid black; padding: 0;">
                <div class="panel-header" style="background-color: crimson; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        SQLInjector
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; color: black; min-height: 300px;">
                    <label style="width: 20%;" class="col-xs-3 col-form-label">Target IP: </label>
                    <asp:TextBox ID="TargetIPTxtBox" runat="server" Style="width: 166px;"></asp:TextBox>
                    <fieldset style="margin-top: 2%;">
                        <legend style="font-size: 15px;">Connection Options</legend>
                        <label style="width: 20%;" class="col-xs-3 col-form-label">Brwoser Type: </label>
                        <asp:DropDownList ID="TargetAtkTypeList" runat="server">
                            <asp:ListItem>==Select Attack Type==</asp:ListItem>
                            <asp:ListItem>FireFox</asp:ListItem>
                            <asp:ListItem>Opera</asp:ListItem>
                            <asp:ListItem>Google Chrome</asp:ListItem>
                            <asp:ListItem>Internet Explorer</asp:ListItem>
                        </asp:DropDownList>
                    </fieldset>
                    <br />
                    <asp:Label runat="server" ID="ErrorLbl"></asp:Label><br />
                    <asp:RegularExpressionValidator ID="IPValidator" runat="server"
                        ForeColor="Red"
                        ErrorMessage="Please enter a valid IP address"
                        ControlToValidate="TargetIPTxtBox"
                        ValidationExpression="([0-9]{1,3}\.|\*\.){3}([0-9]{1,3}|\*){1}" /><br />
                    <asp:Button runat="server" ID="ConfigSQL" CssClass="btn-primary" Text="Configure" OnClick="ConfigSQL_Click" />
                </div>
            </div>
            <div class="col-xs-6 col-md-4" style="border: 1px solid black; padding: 0;">
                <div class="panel-header" style="background-color: crimson; margin: 0; padding: 5px;">
                    <h3 style="margin: 0;">
                        <asp:Image ImageUrl="~/Content/Images/kali.png" Width="25px" runat="server" />
                        Instruction
                    </h3>
                </div>
                <div class="panel-body" style="background-color: #f5f5f5; min-height: 300px;">
                    <h4 style="color: black;">Steps for SQL Injection Attack</h4>
                    <ol class="list-group" style="color: black;">
                        <li class="list-group-item">1. Configure SQLInjector</li>
                        <li class="list-group-item">2. Run SQLInjector</li>
                        <li class="list-group-item">3. Run Browser</li>
                        <li class="list-group-item">4. Destory Database</li>
                    </ol>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-9">
                <div class="panel panel-default">
                    <div class="panel-header" style="background-color: grey;">
                        <h4 style="text-align: center; margin-bottom: 0;">@HackNetHost:~
                        </h4>
                    </div>
                    <div class="panel-body" style="border-radius: 0; background-color: #091012; overflow-y: auto; max-height: 350px; height: 250px;">
                        <asp:Panel ID="LogPanel" runat="server"></asp:Panel>
                    </div>
                    <div class="panel-footer" style="background-color: #091012; border-top: 1px solid white;">
                        <asp:Label runat="server" Text="@HackNet:~#" Width="20%"></asp:Label>
                        <asp:TextBox runat="server" ID="CmdTextBox" BackColor="#091012" BorderStyle="None" Style="min-width: 69%; width: 69%; padding: 5px;"></asp:TextBox>
                        <asp:Button runat="server" OnClick="SubCmdBtn_Click" ID="SubCmdBtn" Text="Submit" CssClass="btn btn-default" Width="10%" Style="float: right;" />
                        <asp:Label runat="server" ID="CmdError"></asp:Label>
                        <br />
                        <asp:RegularExpressionValidator runat="server" ID="CmdRegValidator"
                            ControlToValidate="CmdTextBox"
                            ForeColor="Red"
                            ErrorMessage="Invalid Input"
                            ValidationExpression="^[a-zA-Z0-9_\s]*$"></asp:RegularExpressionValidator>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="panel panel-default" style="height: 340px;">
                    <div class="panel-header" style="background-color: crimson;">
                        <h4 style="text-align: center; margin-bottom: 0;">Possible Login URLs
                        </h4>
                    </div>
                    <div class="panel-body" style="background-color: black;">
                        <asp:DataList ID="URLListView" runat="server">
                            <HeaderTemplate>
                                <h4>Possible URL List</h4>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li class="list-group-item" style="background: #666666; width: 220px; margin: 0 auto;">
                                    <asp:Label runat="server" Text='<%#Eval("PossURL") %>'></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row" style="margin-left: 0; margin-right: 0;">
            <div class="panel panel-default">
                <div class="panel-header" style="background-color: crimson;">
                    <h4 style="text-align: center; margin-bottom: 0;">Browser</h4>
                </div>
                <div class="panel-body">
                    <asp:Label runat="server" Text="Username: "></asp:Label>
                    <asp:TextBox runat="server" ID="UsrNamTxtBox"></asp:TextBox>
                    <br />
                    <asp:Label runat="server" Text="Password"></asp:Label>
                    <asp:TextBox runat="server" ID="PassTxtBox"></asp:TextBox>
                    <br />
                    <asp:Button runat="server" CssClass="btn-primary" ID="LoginBtn" OnClick="LoginBtn_Click" Text="Login" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
