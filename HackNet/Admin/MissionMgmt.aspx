<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.master" AutoEventWireup="true" CodeBehind="MissionMgmt.aspx.cs" Inherits="HackNet.Admin.MissionMgmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AdminPanelContent" runat="server">
      <div class="container-fluid" style="color: black; background-color: gray;">
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
