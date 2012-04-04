$(function () {

    // Hubs are defined as javascript child objects on the connection object.
    // The hubs that are there are determined by what the server offers.
    // Check out the AuctioneerHub.cs file for details on the hub implementation
    var auctioneerHub = $.connection.auctioneer;

    // Save the ID of the currently selected auction
    var auctionId = parseInt($('#auction-id').val());

    // This is a callback that the server can invoke on the auctioneer hub.
    // The data will contain an anonymous object constructed by the server.
    auctioneerHub.bidPlaced = function (bid) {

        // All auctions are managed through the same hub, so this is needed
        // to differentiate between the various hubs. Normally you would do this
        // by creating different hubs for the different auctions
        if (bid.auctionId == auctionId) {

            // Update the highest bid field with the new bid amount
            $('#highest-bid').html(bid.amount);

            // Okay, so normally you would use jQuery templates or knockout.js to do this.
            // But that would make this a little more complicated to explain so this will have to do.
            var bidDetails = '<div class="bid">' +
                "<div>" + bid.postedBy + "</div>" +
                "<div>&euro; " + bid.amount + "</div>" +
                "</div>";

            // Add the new bid information to the start of the feed.
            $('#bids').prepend(bidDetails);
        }
    };

    // Attach an eventhandler to the submit event.
    // This captures the form content and sends it to the server using SignalR
    $('#place-bid-form').submit(function () {

        // Retrieve the new bid amount
        var amount = parseInt($('#amount').val());

        // Clean up the form
        $('#errormessage').html();

        // Place a bid on the server by invoking the PlaceBid method on the hub.
        // There's a done callback attached to this function so that the page can update
        // itself once the operation has completed. This is a bit like Task.ContinueWith in .NET
        auctioneerHub.placeBid(auctionId, amount)
            .done(function () {
                // Close the dialog when the submit operation is done.
                $('#place-bid-dialog').dialog('close');
            })
            .fail(function (error) {
                $('#errormessage').html(error);
            });

        // Don't actually submit the form,  we've already submitted it through signalr ;-)
        return false;
    });


    // Attach an event handler to the click event of the place bid link.
    // Display the place bid dialog when the user clicks it.
    $('#place-bid-link').click(function () {
        $('#place-bid-dialog').dialog('open');
    });

    // Some final initialization steps to get this thing going in the right direction.
    $('#place-bid-dialog').dialog({ autoOpen: false });

    // Start the hub connection
    $.connection.hub.start();
});