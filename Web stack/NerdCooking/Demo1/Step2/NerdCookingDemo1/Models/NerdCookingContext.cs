using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdCookingDemo1.Models
{
    public class NerdCookingContext: DbContext
    {
        public DbSet<Workshop> Workshops { get; set; }
    }
}