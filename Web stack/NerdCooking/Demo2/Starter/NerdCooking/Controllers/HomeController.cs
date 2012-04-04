using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NerdCooking.Models;

namespace NerdCooking.Controllers
{
    public class HomeController : Controller
    {
        private IWorkshopRepository _workshopRepository;

        public HomeController()
        {
            _workshopRepository = new WorkshopRepository();
        }

        public HomeController(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        //
        // Action: /Home/
        public ViewResult Index()
        {
            return View(new IndexViewModel
            {
                UpcomingWorkshops = _workshopRepository.GetUpcomingWorkshops()
            });
        }

    }
}
