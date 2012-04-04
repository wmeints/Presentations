<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Listen.aspx.cs" Inherits="HtmlBroadcast.Listen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Scripts/receive.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Broadcast receiver</h2>
    <p>On this page you can listen to data being broadcasted through SignalR persistent connections.</p>
    <p>The following data was received from other clients:</p>
    <div id="broadcast-content">
    </div>
</asp:Content>
