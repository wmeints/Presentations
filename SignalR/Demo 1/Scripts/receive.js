$(function () {
    // Create a new connection to the server
    var streamingConnection = $.connection('broadcast');

    // Hook up the received event of the streaming connection
    streamingConnection.received(function (data) {
        // Simply append the broadcasted data to the output div
        $('#broadcast-content').append('<span>' + data + '</span>');
    });

    // Always start the connection when you want to receive data.
    streamingConnection.start();
});