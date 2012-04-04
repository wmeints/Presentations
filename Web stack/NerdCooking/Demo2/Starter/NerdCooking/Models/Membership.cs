using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NerdCooking.Models
{
    public class Membership
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public bool IsOptional { get; set; }
        public bool IsConfirmed { get; set; }
    }
}