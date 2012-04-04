using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using NerdCooking.Models;

namespace NerdCooking.Controllers
{
    public class WorkshopsController : Controller
    {
        private IMembershipRepository _membershipRepository;
        private IWorkshopRepository _workshopRepository;
        
        public WorkshopsController()
            :this(new MembershipRepository(), new WorkshopRepository())
        {
        }

        public WorkshopsController(IMembershipRepository membershipRepository, IWorkshopRepository workshopRepository)
        {
            _membershipRepository = membershipRepository;
            _workshopRepository = workshopRepository;
        }

        //
        // GET: /Workshops/Create
        [Authorize(Roles = "Organizer")]
        public ActionResult Create()
        {
            return View(new Workshop());
        }

        //
        // POST: /Workshops/Create
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public ActionResult Create(Workshop workshop)
        {
            try
            {
                // Automatically create reservations for the members in the application
                // that are not marked as optional and are confirmed
                foreach (var membership in _membershipRepository.GetMemberships())
                {
                    Reservation reservation = new Reservation();
                    reservation.UserName = membership.UserName;

                    workshop.Reservations.Add(reservation);
                }

                // Save the workshop to the database
                _workshopRepository.InsertOrUpdate(workshop);
                _workshopRepository.Save();

                return RedirectToAction("Details", new { id = workshop.Id });
            }
            catch (Exception)
            {
                return View(workshop);
            }
        }

        //
        // GET: /Workshops/Details/id
        public ViewResult Details(int id)
        {
            var workshop = _workshopRepository.Find(id);
        	return View(workshop);
        }
    }
}
