$(function () {

    // Initialize the SignalR persistent connection.
    // There's a server counterpart for this code in the ASP.NET web application.
    var streamingConnection = $.connection('broadcast');

    // Always start the connection when you want to send or receive data.
    streamingConnection.start();

    // -- PART 1: Sending content to a server using a persistent connection --
    // -----------------------------------------------------------------------

    // Hook up a callback for the stream input form.
    $('#start-streaming-button').click(function () {

        // Get the content to stream
        var contentToStream = $('#streaming-content').val();
        var contentLength = contentToStream.length;
        var index = 0;

        var contentStreamTimer = $.timer(function () {
            // Stop the timer once the end of the text stream is reached
            if (index == contentLength) {
                contentStreamTimer.stop();
            }

            // Send the next character of the content to the other clients
            streamingConnection.send(contentToStream.substring(index, index + 1));

            index++;
        });

        // Start the timer on a 100 msec trigger
        contentStreamTimer.set({ time: 100, autostart: true });
    });
});