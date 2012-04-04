using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NerdCooking.Models
{ 
    public class MembershipRepository : IMembershipRepository
    {
        public IQueryable<Membership> GetMemberships()
        {
            return NerdCookingContext.Current.Memberships
                .Where(membership => !membership.IsOptional && membership.IsConfirmed);
        }

        public IQueryable<Membership> GetOptionalMemberships()
        {
            return NerdCookingContext.Current.Memberships
                .Where(membership => membership.IsOptional && membership.IsConfirmed);
        }

        public Membership Find(int id)
        {
            return NerdCookingContext.Current.Memberships.Find(id);
        }

        public void InsertOrUpdate(Membership membership)
        {
            if (membership.Id == default(int)) {
                // New entity
                NerdCookingContext.Current.Memberships.Add(membership);
            } else {
                // Existing entity
                NerdCookingContext.Current.Entry(membership).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var membership = NerdCookingContext.Current.Memberships.Find(id);
            NerdCookingContext.Current.Memberships.Remove(membership);
        }

        public void Save()
        {
            NerdCookingContext.Current.SaveChanges();
        }
    }

    public interface IMembershipRepository
    {
        IQueryable<Membership> GetMemberships();
        IQueryable<Membership> GetOptionalMemberships();
        Membership Find(int id);
        void InsertOrUpdate(Membership membership);
        void Delete(int id);
        void Save();
    }
}