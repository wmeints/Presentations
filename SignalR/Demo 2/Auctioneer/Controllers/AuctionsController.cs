using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Auctioneer.Models;

namespace Auctioneer.Models
{
    public class AuctionsController : Controller
    {

        /// <summary>
        /// Displays all the running auctions
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            List<Auction> auctions = null;

            using (var context = new AuctioneerContext())
            {
                auctions = context.Auctions
                    .OrderByDescending(auction => auction.EndDate)
                    .ToList();
            }

            return View(auctions);
        }

        /// <summary>
        /// Creates a new auctionsController
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Create()
        {
            return View(new CreateAuctionForm());
        }

        /// <summary>
        /// Displays the details for an existing auction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            Auction foundAuction = null;

            // Retrieve the auction from the database
            using (var context = new AuctioneerContext())
            {
                foundAuction = context.Auctions
                    .Include("Bids")
                    .FirstOrDefault(auction => auction.Id == id);
            }

            if (foundAuction == null)
            {
                // A true website does not display data that does not exist.
                // So instead throw an error and let the rest of the infrastructure handle the mess.
                throw new HttpException(404, "Auction not found");
            }

            return View(foundAuction);
        }

        /// <summary>
        /// Completes the creation of a new auction
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult Create(CreateAuctionForm form)
        {
            // Validate the model. If that fails, return the user to the original form.
            if (!TryValidateModel(form))
            {
                return View(form);
            }

            Auction newAuction = null;

            // Save the new auction in the database
            using (var context = new AuctioneerContext())
            {
                newAuction = new Auction
                {
                    Name = form.Name,
                    EndDate = form.EndDate,
                    Description = form.Description
                };

                context.Auctions.Add(newAuction);
                context.SaveChanges();
            }

            // Redirect to the details page
            return RedirectToAction("Details", new { id = newAuction.Id });
        }
    }
}
