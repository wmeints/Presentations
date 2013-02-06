using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;

namespace RecipeBrowser.Web.Models
{
    [MetadataTypeAttribute(typeof(Unit.UnitMetadata))]
    public partial class Unit : ITrackableEntity
    {
        internal sealed class UnitMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private UnitMetadata()
            {
            }

            public string CreatedBy { get; set; }

            public DateTime DateCreated { get; set; }

            public Nullable<DateTime> DateModified { get; set; }

            public EntityCollection<Ingredient> Ingredients { get; set; }

            public string ModifiedBy { get; set; }

            public string Name { get; set; }

            public decimal UnitId { get; set; }

            public byte[] Version { get; set; }
        }
    }
}
