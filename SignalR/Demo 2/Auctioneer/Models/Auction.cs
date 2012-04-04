using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Auctioneer.Models
{
    /// <summary>
    /// Represents a single auctioned item
    /// </summary>
    public class Auction
    {
        /// <summary>
        /// Initializes a new instance of the Auction class.
        /// </summary>
        public Auction()
        {
            Bids = new Collection<Bid>();
        }

        /// <summary>
        /// Gets or sets the ID of the bid
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item that is auctioned
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the item being auctioned
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets the enddate for the auction
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the bids placed for the auction
        /// </summary>
        public virtual Collection<Bid> Bids { get; set; }
    }
}