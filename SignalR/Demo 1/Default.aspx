<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="HtmlBroadcast._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script src="Scripts/broadcast.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to the SignalR persistent connection sample
    </h2>
    <p>
        This sample demonstrates the most basic part of SignalR; The persistent connection API.
        A persistent connection enables you to basically stream data from client to server and vice versa.
    </p>
    
        <h3>Stream some content to other clients</h3>
        <p>Enter some text here and stream it to the other clients</p>
        <textarea id="streaming-content" style="min-height: 70px; width: 474px; height: 78px;">
        </textarea>
        <p><input id="start-streaming-button" type="button" value="Start streaming" /></p>


        <h3>Listen for content from other clients</h3>
        <p><a href="Listen.aspx" id="listen-link">Click here</a> to listen for other content from other clients.</p>
        
</asp:Content>
