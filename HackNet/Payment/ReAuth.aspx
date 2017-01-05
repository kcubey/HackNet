<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="ReAuth.aspx.cs" Inherits="HackNet.Payment.ReAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameHeadPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">
        <h1>You have chosen to buy
            <asp:Label ID="pkgName" runat="server" Text="Package A" 
                ForeColor="Blue" CssClass="btn btn-success" Font-Bold="True" Font-Size="Large" BackColor="Black"></asp:Label>
        </h1> 
    <h5>Please enter your details below for re-authentication</h5>
    <hr />

    <asp:Label ID="Msg" ForeColor="Red" runat="server"/>
    <table runat="server" class="loginTable">
				<tr>
					<td><strong>Email:</strong></td>
					<td>
						<asp:TextBox ID="Email" runat="server" CssClass="form-control" /></td>
					<td>
						<asp:RequiredFieldValidator
							ID="EmailValidator"
							ControlToValidate="Email"
							ErrorMessage="*"
							runat="server" />
						<asp:RegularExpressionValidator
							ID="EmailRegExValidator"
							ControlToValidate="Email"
							ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
							ErrorMessage="Invalid Email Address"
							runat="server" />
					</td>
				</tr>
				<tr>
					<td><strong>Password:</strong></td>
					<td>
						<asp:TextBox ID="UserPass" CssClass="pwdfield form-control" TextMode="Password" runat="server" />
					</td>
					<td>
						<asp:RequiredFieldValidator
							ID="UserPassValidator"
							ControlToValidate="UserPass"
							ErrorMessage="*"
							runat="server" />
					</td>
				</tr>
			</table>
			<asp:Button ID="AuthButton"
				Text="Authenticate"
				CssClass="btn btn-primary loginBtn"
				OnClick="AuthClick"
				CausesValidation="true"
				runat="server" />


    <!--
    <h4>Email: </h4>    <asp:TextBox ID="TextBox1" runat="server" ForeColor="Black"></asp:TextBox>
    <br /><br />
    <h4>Password: </h4>    <asp:TextBox ID="TextBox2" runat="server" ForeColor="Black"></asp:TextBox>
    <br /><br />
    <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Authenticate" Font-Size="Large" PostBackUrl="~/Payment/payment.aspx"></asp:LinkButton>
    <br /><br />
    <asp:LinkButton runat="server" CssClass="btn btn-success" Text="Back" Font-Size="Large" PostBackUrl="~/Payment/market.aspx"></asp:LinkButton>
    -->

</asp:Content>
