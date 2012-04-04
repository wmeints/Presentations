using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;
using Auctioneer.Models;
using System.Transactions;

namespace Auctioneer
{
    /// <summary>
    /// This is a SignalR hub for posting bids to auctions.
    /// The HubName attributes specifies the name the hub object has in Javascript. 
    /// It also makes this hub adressable from WP7 or Silverlight.
    /// 
    /// All public methods on this hub can be invoked from the client.
    /// </summary>
    [HubName("auctioneer")]
    public class AuctioneerHub: Hub
    {
        public void PlaceBid(int auctionId, decimal amount)
        {
            // Do not accept bids from anonymous bidders
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User is not logged on");
            }

            // Save the bid in the database
            SaveBid(auctionId, amount);

            // Notify the clients of the fact that a new bid was placed.
            // NOTE: The method invoked does NOT exist, rather it signifies an address of a callback on the client.
            //       the data being sent is transported as a JSON object hence it's an anonymous object.
            Clients.bidPlaced(new { auctionId = auctionId, amount = amount, postedBy = HttpContext.Current.User.Identity.Name });
        }

        private static void SaveBid(int auctionId, decimal amount)
        {
            using (var context = new AuctioneerContext())
            {
                var foundAuction = context.Auctions.FirstOrDefault(auction => auction.Id == auctionId);

                // Can't accept bids for auctions that don't exist anymore!
                if (foundAuction == null)
                {
                    throw new ArgumentException("The specified auction does not exist", "auctionId");
                }

                // Can't accept bids for auctions that have been closed or sold
                if (foundAuction.EndDate < DateTime.Now)
                {
                    throw new InvalidOperationException("This auction has already been closed.");
                }

                decimal highestBid = 0;

                if (foundAuction.Bids.Count > 0)
                {
                    highestBid = foundAuction.Bids.Max(bid => bid.Amount);
                }

                // Only higher bids are allowed for the auction
                if (highestBid >= amount)
                {
                    throw new ArgumentException("The placed bid isn't higher than the previous bid", "amount");
                }

                Bid newBid = new Bid()
                {
                    Amount = amount,
                    Auction = foundAuction,
                    DatePosted = DateTime.Now,
                    PostedBy = HttpContext.Current.User.Identity.Name
                };

                // Save the new bid in the database
                context.Bids.Add(newBid);
                foundAuction.Bids.Add(newBid);

                using (var transaction = new TransactionScope())
                {
                    context.SaveChanges();
                    transaction.Complete();
                }
            }
        }
    }
}