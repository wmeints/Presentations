using System;
using System.Data.Entity;
using Auctioneer.Models;
using System.ComponentModel.DataAnnotations;

namespace Auctioneer.Models
{
    /// <summary>
    /// Represents a bid on an auctioned item
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Gets or sets the ID of the bid
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who placed the bid
        /// </summary>
        [Required]
        public string PostedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the bid was placed
        /// </summary>
        [Required]
        public DateTime DatePosted { get; set; }

        /// <summary>
        /// Gets or sets the amount poste
        /// </summary>
        [Required]
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the auction the bids belongs to
        /// </summary>
        public virtual Auction Auction { get; set; }
    }
}


