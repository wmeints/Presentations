using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdCooking.Models
{
    public class NerdCookingContext : DbContext
    {
		private static NerdCookingContext Instance;
		private static object SyncRoot = new object();

		public static NerdCookingContext Current 
		{
			get
			{
				if(Instance == null)
				{
					lock(SyncRoot)
					{
						if(Instance == null)
						{
							Instance = new NerdCookingContext();
						}
					}
				}

				return Instance;
			}
		}

        public DbSet<NerdCooking.Models.Workshop> Workshops { get; set; }

        public DbSet<NerdCooking.Models.Reservation> Reservations { get; set; }

        public DbSet<NerdCooking.Models.Membership> Memberships { get; set; }

        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<NerdCooking.Models.NerdCookingContext>());

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}