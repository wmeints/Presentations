using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecipeBrowser.Web.Models
{
    /// <summary>
    /// Marks a trackable entity
    /// </summary>
    public interface ITrackableEntity
    {
        /// <summary>
        /// Gets or sets the date the entity was created
        /// </summary>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date the entity was modified
        /// </summary>
        DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the user who created the entity
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the user who modified the entity
        /// </summary>
        string ModifiedBy { get; set; }
    }
}
