using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace RecipeBrowser.Web.Models
{
    [MetadataTypeAttribute(typeof(Category.CategoryMetadata))]
    public partial class Category : ITrackableEntity
    {
        /// <summary>
        /// Gets the number of recipes in the category
        /// </summary>
        [DataMember]
        public int RecipeCount { get; set; }

        internal sealed class CategoryMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CategoryMetadata()
            {
            }

            public int RecipeCount { get; set; }

            public decimal CategoryId { get; set; }

            public string CreatedBy { get; set; }

            public DateTime DateCreated { get; set; }

            public Nullable<DateTime> DateModified { get; set; }

            public bool IsCookingTechnique { get; set; }

            public bool IsKitchenType { get; set; }

            public bool IsMenuCourse { get; set; }

            public bool IsTheme { get; set; }

            public string ModifiedBy { get; set; }

            public string Name { get; set; }

            public EntityCollection<Recipe> Recipes { get; set; }

            public byte[] Version { get; set; }
        }
    }
}
