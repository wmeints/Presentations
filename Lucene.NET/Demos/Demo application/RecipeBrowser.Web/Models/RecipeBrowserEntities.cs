using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace RecipeBrowser.Web.Models
{
    /// <summary>
    /// Recipe browser entity context
    /// </summary>
    public partial class RecipeBrowserEntities
    {
        /// <summary>
        /// Handles additional logic when creating the entity context
        /// </summary>
        partial void OnContextCreated()
        {
            this.SavingChanges += new EventHandler(OnSavingChanges);
        }

        /// <summary>
        /// Updates tracking information when the entities are being saved
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnSavingChanges(object sender, EventArgs e)
        {
            var insertedEntries = ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Added);
            var updatedEntries = ObjectStateManager.GetObjectStateEntries(System.Data.EntityState.Modified);

            foreach (var item in insertedEntries)
            {
                var trackable = item.Entity as ITrackableEntity;

                if(trackable != null)
                {
                    trackable.CreatedBy = Thread.CurrentPrincipal.Identity.Name;
                    trackable.DateCreated = DateTime.Now;
                }
            }

            foreach (var item in updatedEntries)
            {
                var trackable = item.Entity as ITrackableEntity;

                if(trackable != null)
                {
                    trackable.ModifiedBy = Thread.CurrentPrincipal.Identity.Name;
                    trackable.DateModified = DateTime.Now;
                }
            }
        }
    }
}