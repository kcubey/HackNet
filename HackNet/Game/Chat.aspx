<%@ Page Title="Messaging" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="HackNet.Game.Chat" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="GameHeadPH" runat="server">
	<link rel="stylesheet" href="../Content/Chat.css" />



</asp:Content>
<asp:Content ID="ChatContent" ContentPlaceHolderID="GameContent" runat="server">

	<div style="display:none">
        <asp:Button ID="HidnReload" runat="server" />
    </div>
	
	<div class="panel panel-default" ID="SelectRecipientWindow" runat="server">
		<div class="panel-heading">
			<h3 class="panel-title">HackNet Chat</h3>
		</div>
		<div class="panel-body">
			<div runat="server" class="col-md-3">
				Select a recipient
				<asp:TextBox ID="ReceiverId" placeholder="Enter Username"
							runat="server" CssClass="form-control"/>
			</div>
			<div class="col-md-6">
				<br />
				<asp:Button ID="ButtonChooseRecipient" runat="server"
							OnClick="ButtonChooseRecipient_Click" class="btn btn-info" 
							Text="Find Recipient" /> <br />
			</div>
			<div class="col-md-3">
				Recents:
				<asp:DataList runat="server" ID="RecentsDataList">
					<ItemTemplate>
						<asp:LinkButton runat="server" OnClick="SetRecipient" CommandArgument="<%# Container.DataItem %>">
							<%# Container.DataItem %>
						</asp:LinkButton>
					</ItemTemplate>
				</asp:DataList>
			</div>
		</div>
		<asp:Label ID="Msg" runat="server" />
	</div>
	<div class="chatsection" ID="ChatWindow" runat="server" visible="false" onload="ChatWindow_Load">
		<div class="ChatTitle">Chatting with <asp:Label ID="LblRecipient" runat="server" /></div>
		<asp:UpdatePanel ID="ChatUpdatePanel" ChildrenAsTriggers="true" UpdateMode="Conditional" runat="server">
		<Triggers>
				<asp:AsyncPostBackTrigger runat="server" ControlID="ReloadBtn" EventName="Click" />
				<asp:PostBackTrigger runat="server" ControlID="SendMsg" />
		</Triggers>
		<ContentTemplate>
		<div class="panel panel-default">
			<div class="panel-body" style="background-color:midnightblue">
				<div class="col-md-3">
					<br />
					<asp:Button ID="ChangeRecipientBtn" runat="server" Text="Change Recipient"
						 CssClass="btn btn-info"   OnClick="ChangeRecipientBtn_Click" />
					<asp:Button ID="ReloadBtn" runat="server" CausesValidation="false"
						 Text="Reload" CssClass="btn btn-info" ClientIDMode="Static" />
				</div>
				<div class="col-md-5"></div>
				<div class="col-md-3">
					<asp:Panel runat="server" DefaultButton="SendMsg">
					Send a message:
					<asp:TextBox ID="MessageToSend" runat="server" 
								 CssClass="form-control" />
					</asp:Panel>
				</div>
				<div class="col-md-1">
					<br />
					<asp:Button ID="SendMsg" Text="Send" runat="server" 
						CssClass="btn btn-info" OnClick="SendMsg_Click" OnClientClick="Update_UpdatePanel()" 
						/>
				</div>
			</div>
		</div>

		<ol class="chat">
			<asp:Repeater ID="ChatRepeater" runat="server" OnLoad="ChatRepeater_Load">
				<ItemTemplate>
					<li class="<%# ThisOrOther( (int) Eval("SenderId") ) %>">
						<div class="msg">
							<div class="user"><asp:Label ID="LblMsgSender" Text='<%# GetUsername( (int) Eval("SenderId") ) %>' runat="server"/></div>
							<p><asp:Label ID="LblMsgContent" Text='<%# Eval("Content") %>' runat="server"/></p>
							<time><asp:Label ID="LblMsgTimestamp" Text='<%# Eval("Timestamp") %>' runat="server"/></time>
						</div>
					</li>
				</ItemTemplate>
			</asp:Repeater>
		</ol>
		</ContentTemplate>
		</asp:UpdatePanel>
	</div>
	<!--
	<div class="chatsection">
		<div class="ChatTitle">Chatting with</div>
		<ol class="chat">
			<li class="other">
				<div class="msg">
					<div class="user">Wuggle<span class="range admin">Admin</span></div>
					<p>Dude</p>
					<p>
						Want to go dinner?
					</p>
					<time>20:17</time>
				</div>
			</li>
			<li class="self">
				<div class="msg">
					<p>Hmm...</p>
					<p>
						HOW ABOUT NO!
					</p>
					<p>I'd prefer another day actually</p>
					<time>20:18</time>
				</div>
			</li>
			<li class="other">
				<div class="msg">
					<div class="user">Wuggle<span class="range admin">Admin</span></div>
					<p>
						Awwww okay!
					</p>
					<time>20:18</time>
				</div>
			</li>
			<li class="self">
				<div class="msg">
					<p>Seeya then!</p>
					<p>It's for tomorrow I guess</p>
					<time>20:18</time>
				</div>
			</li>
		</ol>
	</div>
	-->
	<script type="text/javascript">
		function Update_UpdatePanel() {
			setTimeout(function () {
				document.getElementById("ReloadBtn").click();
			}, 400);

			setTimeout(function () {
				document.getElementById("ReloadBtn").click();
			}, 2000);
		}
	</script>
</asp:Content>
