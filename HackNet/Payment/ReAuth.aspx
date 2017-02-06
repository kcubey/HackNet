<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="ReAuth.aspx.cs" Inherits="HackNet.Payment.ReAuth" %>
<asp:Content ID="Content1" ContentPlaceHolderID="GameHeadPH" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="GameContent" runat="server">

    <div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">Re - Authentication</h3>
		</div>
		<div class="panel-body">
            You have chosen to buy <asp:Label ID="packageNameLbl" runat="server" ForeColor="Red" Font-Bold="True" ></asp:Label>
            at <asp:Label ID="packagePriceLbl" runat="server" ForeColor="Red"></asp:Label>.
            <br />Please enter your details below for re-authentication.
            <hr />

            <asp:Label ID="Msg" ForeColor="Red" runat="server"/>
            <table runat="server" class="formTable">
                <tr>
                    <td><strong>Email:</strong></td>
					<td>
                        <asp:TextBox ID="Email" runat="server" autocomplete="off" CssClass="form-control" />
					</td>
					<td>
					    <asp:RequiredFieldValidator
						    ID="RequiredFieldValidator1"
						    ControlToValidate="Email"
						    ErrorMessage="*"
						    runat="server" />
					    <asp:RegularExpressionValidator
						    ID="RegularExpressionValidator1"
						    ControlToValidate="Email"
						    ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"
						    ErrorMessage="Invalid Email Address"
						    runat="server" />
					</td>
				</tr>
				<tr>
					<td><strong>Password:</strong></td>
					<td>
						<asp:TextBox ID="UserPass" CssClass="pwdfield form-control" autocomplete="off" TextMode="Password" runat="server" />
					</td>
					<td>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2"
							ControlToValidate="UserPass"
							ErrorMessage="*"
							runat="server" />
					</td>
                </tr>
            </table>
			
            <asp:Button ID="AuthButton"
                Text="Authenticate"
				CssClass="btn btn-success"
				OnClick="AuthClick"
				CausesValidation="true"
				runat="server" />
            
            <asp:Button ID="CancelButton" causesvalidation="false" Text="Cancel" CssClass="btn btn-success"	OnClick="CancelClick" runat="server" />
        
        </div>
	</div>
</asp:Content>
