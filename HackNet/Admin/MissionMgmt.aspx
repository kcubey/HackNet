<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="MissionMgmt.aspx.cs" Inherits="HackNet.Admin.MissionMgmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
    <script>
        function showEditMissionModel() {
            $('#EditMissionModel').modal('show');
        }
    </script>
    <div id="EditMissionModel" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <asp:Label runat="server" Text="Edit Mission" ForeColor="Black" Font-Size="Larger"></asp:Label>
                </div>
                <div class="modal-body" style="color: black">
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Mission ID: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:TextBox runat="server" ID="EditMisID" Enabled="false" Width="280px"></asp:TextBox>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Mission Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:TextBox runat="server" ID="EditMisName" Width="280px"></asp:TextBox>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Mission IP: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:TextBox runat="server" ID="EditIP" Width="280px"></asp:TextBox>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Mission Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:TextBox runat="server" ID="EditMisType" Enabled="false" Width="280px"></asp:TextBox>
                    </div>
                    <div class="form-group row">
                        <asp:Label runat="server" Text="Mission Description: " CssClass="col-xs-3 col-form-label"></asp:Label>
                        <asp:TextBox runat="server" ID="EditMisDesc" TextMode="MultiLine" Width="280px" Height="150px"></asp:TextBox>
                    </div>
                    <div class="form-group row">
                        <div class="col-xs-9">
                            <asp:RegularExpressionValidator ID="MisNameValidator" runat="server"
                                ForeColor="Red"
                                ErrorMessage="Invalid Input for Mission Name"
                                ControlToValidate="EditMisName"
                                ValidationExpression="^[a-zA-Z'.\s]{1,40}$" /><br />
                            <asp:RegularExpressionValidator ID="IPValidator" runat="server"
                                ForeColor="Red"
                                ErrorMessage="Please enter a valid IP address"
                                ControlToValidate="EditIP"
                                ValidationExpression="([0-9]{1,3}\.|\*\.){3}([0-9]{1,3}|\*){1}" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button runat="server" CssClass="btn btn-default" ID="UpdateMissionInfoBtn" OnClick="UpdateMissionInfoBtn_Click" Text="Update" />
                </div>
            </div>
        </div>
    </div>


    <div class="container-fluid" style="color: black; background-color: black;">
    <asp:Label runat="server" Text="Mission Panel" Font-Size="Large" ForeColor="White"></asp:Label>
        <asp:GridView runat="server" ForeColor="White" BorderStyle="None" CssClass="table" ID="AdminMissionView" OnLoad="AdminMissionView_Load">
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" Text="Edit" ID="EditMisBtn" CommandArgument='<%#Bind("MissionID") %>' OnCommand="EditMisBtn_Command"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>


    <div class="container-fluid" style="color: black; background-color: gray;">
        <h2>Mission Editor</h2>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Name: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisName"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Desc: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisDesc" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Type: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="AtkTypeList">
                <asp:ListItem Value="0">AtkTypPwdAtks</asp:ListItem>
                <asp:ListItem Value="1">AtkTypDdos</asp:ListItem>
                <asp:ListItem Value="2">AtkTypMITM</asp:ListItem>
                <asp:ListItem Value="3">AtkTypSQL</asp:ListItem>
                <asp:ListItem Value="4">AtkTypXSS</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Recommend Level: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:DropDownList runat="server" ID="RecomLvlList">
                <asp:ListItem Value="0">Lvl1to5</asp:ListItem>
                <asp:ListItem Value="1">Lvl6to10</asp:ListItem>
                <asp:ListItem Value="2">Lvl11to15</asp:ListItem>
                <asp:ListItem Value="3">Lvl16to20</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Exp: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisExp"></asp:TextBox>
        </div>
        <div class="form-group row">
            <asp:Label runat="server" Text="Mission Coin: " CssClass="col-xs-3 col-form-label"></asp:Label>
            <asp:TextBox runat="server" ID="MisCoin"></asp:TextBox>
        </div>
        <asp:Button runat="server" ID="btnAddMis" CssClass="btn btn-default" OnClick="btnAddMis_Click" Text="Add Item" />
    </div>

</asp:Content>
