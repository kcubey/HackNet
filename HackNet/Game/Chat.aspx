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
							OnClick="ButtonChooseRecipient_Click" CssClass="btn btn-info" 
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
				<asp:AsyncPostBackTrigger ControlID="ReloadBtn" EventName="Click" />
				<asp:AsyncPostBackTrigger ControlID="SendMsg" />
		</Triggers>
		<ContentTemplate>

		<!-- Message repeater for each message -->
		<ol class="chat" style="max-height:400px; overflow: scroll;" id="chatdiv">
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

		<!-- Controls for user -->
		<div class="panel panel-default">
			<div class="panel-body" style="background-color:navy">
				<div class="col-md-3">
					<br />
					<asp:Button ID="ChangeRecipientBtn" runat="server" Text="Change Recipient"
						 CssClass="btn btn-info"   OnClick="ChangeRecipientBtn_Click" />
					<asp:Button ID="ReloadBtn" runat="server" CausesValidation="false"
						 Text="Reload" CssClass="btn btn-info" ClientIDMode="Static" />
				</div>
				<div class="col-md-5">
						<asp:Label ID="Msg1" runat="server" />
				</div>
				<div class="col-md-3">
					<asp:Panel runat="server" DefaultButton="SendMsg">
					Send a message:
					<asp:TextBox ID="MessageToSend" runat="server" autocomplete="off"
								 ClientIDMode="Static" CssClass="form-control" />
					</asp:Panel>
				</div>
				<div class="col-md-1">
					<br />
					<asp:Button ID="SendMsg" Text="Send" runat="server" ClientIDMode="Static"
						CssClass="btn btn-info" OnClick="SendMsg_Click" 
						/>
				</div>
			</div>
		</div>

		</ContentTemplate>
		</asp:UpdatePanel>
	</div>

	<!--Script references. -->
	<script src="../Scripts/jquery-3.1.1.min.js"></script>
	<script src="../SignalR/jquery.color-2.1.2.min.js"></script>
	<script src="../Scripts/jquery.signalR-2.2.1.js"></script>
	<script src="../SignalR/hubs"></script>

	<!--Add script to update the page and send messages.-->
	<script type="text/javascript">
		$('#chatdiv').scrollTop($('#chatdiv')[0].scrollHeight);

        var chat = $.connection.ChatClientz;

        // Declare a proxy to reference the hub.
        // Create a function that the hub can call to broadcast messages.
        chat.client.broadcastMessage = function (name, msg) {
            // Html encode display name and message.
            var encodedName = $('<div />').text(name).html();
            var encodedMsg = $('<div />').text(msg).html();
            $('#discussion').append('<li><strong>' + encodedName + '</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
        };

		chat.client.doRefresh = function () {
			Update_UpdatePanel();
			$('#SendMsg').click(function () {
				chat.server.causeRefresh();
			})
		};

        // Start the connection.
        $.connection.hub.start().done(function () {
			$(document).on('click', '#SendMsg', function () {
				chat.server.causeRefresh();
			});
        });

		function Update_UpdatePanel() {
			setTimeout(function () {
				document.getElementById("ReloadBtn").click();
				$('#chatdiv').scrollTop($('#chatdiv')[0].scrollHeight);
			}, 2000);
		}
	</script>
</asp:Content>
