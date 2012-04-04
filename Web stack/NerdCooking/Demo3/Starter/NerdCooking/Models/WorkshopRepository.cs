using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NerdCooking.Models
{ 
    public class WorkshopRepository : IWorkshopRepository
    {
        public IQueryable<Workshop> GetWorkshops()
        {
            return NerdCookingContext.Current.Workshops;
        }

        public IQueryable<Workshop> GetUpcomingWorkshops()
        {
            return NerdCookingContext.Current.Workshops
                .Where(item => item.Date >= DateTime.Now);
        }

        public Workshop Find(int id)
        {
            return NerdCookingContext.Current.Workshops
                .Include("Reservations")
                .FirstOrDefault(workshop => workshop.Id == id);
        }

        public void InsertOrUpdate(Workshop workshop)
        {
            if (workshop.Id == default(int)) {
                NerdCookingContext.Current.Workshops.Add(workshop);

                // Add the reservations to the database as well
                foreach(var reservation in workshop.Reservations)
                {
                    NerdCookingContext.Current.Reservations.Add(reservation);
                }
            } else {
                //NOTE: Reservations are not automatically updated.
                NerdCookingContext.Current.Entry(workshop).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var workshop = NerdCookingContext.Current.Workshops.Find(id);
            NerdCookingContext.Current.Workshops.Remove(workshop);
        }

        public void Save()
        {
            NerdCookingContext.Current.SaveChanges();
        }
    }

    public interface IWorkshopRepository
    {
        IQueryable<Workshop> GetWorkshops();
        IQueryable<Workshop> GetUpcomingWorkshops();
        Workshop Find(int id);
        void InsertOrUpdate(Workshop workshop);
        void Delete(int id);
        void Save();
    }
}