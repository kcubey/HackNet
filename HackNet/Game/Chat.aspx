<%@ Page Title="" Language="C#" MasterPageFile="~/Game.Master" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="HackNet.Game.Chat" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="GameHeadPH" runat="server">
	<link rel="stylesheet" href="../Content/Chat.css" />
</asp:Content>
<asp:Content ID="ChatContent" ContentPlaceHolderID="GameContent" runat="server">
	<div class="panel panel-default">
		<div class="panel-heading">
			<h3 class="panel-title">HackNet Chat</h3>
		</div>
		<div class="panel-body">
			Select a recipient <asp:TextBox placeholder="Enter Username" runat="server" CssClass="form-control" />
		</div>
	</div>
	<div class="chatsection">
		<div class="ChatTitle">Chatting with Wuggle</div>
		<ol class="chat">
			<asp:DataList ID="ChatDataList" runat="server" DataKeyField="MessageId" DataSourceID="MessageDataSource">
				<ItemTemplate>
					<li class="other">
						<div class="msg">
							<div class="user"><asp:Label ID="LblMsgSender" Text='<%# Eval("SenderId") %>' runat="server"/></div>
							<p><asp:Label ID="LblMsgContent" Text='<%# Eval("Content") %>' runat="server"/></p>
							<time><asp:Label ID="LblMsgTimestamp" Text='<%# Eval("Timestamp") %>' runat="server"/></time>
						</div>
					</li>
				</ItemTemplate>
			</asp:DataList>
			<asp:ObjectDataSource ID="MessageDataSource" runat="server"
				OldValuesParameterFormatString="original_{0}"
				SelectMethod="RetrieveMessages" TypeName="HackNet.Game.Chat">
			</asp:ObjectDataSource>
		</ol>
	</div>
	<div class="chatsection">
		<div class="ChatTitle">Chatting with Wuggle</div>
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
	<div>
		<textarea placeholder="Say something"></textarea><input type="submit" class="send" value="" />
	</div>
</asp:Content>
