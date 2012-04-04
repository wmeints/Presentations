using System;
using System.Collections.Generic;
using System.Linq;

namespace NerdCooking.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Workshop> UpcomingWorkshops { get; set; }
    }
}