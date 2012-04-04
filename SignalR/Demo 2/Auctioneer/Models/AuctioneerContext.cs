using System;
using System.Data.Entity;

namespace Auctioneer.Models
{
    /// <summary>
    /// Database context for the auctioneer application
    /// </summary>
    public class AuctioneerContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of AuctioneerContext
        /// </summary>
        public AuctioneerContext()
        {

        }

        /// <summary>
        /// Gets or sets the auctions in the database
        /// </summary>
        public virtual DbSet<Auction> Auctions { get; set; }

        /// <summary>
        /// Gets or sets the bids in the database
        /// </summary>
        public virtual DbSet<Bid> Bids { get; set; }
    }
}
