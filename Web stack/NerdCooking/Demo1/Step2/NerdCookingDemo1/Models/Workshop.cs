using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdCookingDemo1.Models
{
    public class Workshop
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}