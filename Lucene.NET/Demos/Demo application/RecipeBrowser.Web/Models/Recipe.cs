using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.ServiceModel.DomainServices.Server;

namespace RecipeBrowser.Web.Models
{
    [MetadataTypeAttribute(typeof(Recipe.RecipeMetadata))]
    public partial class Recipe : ITrackableEntity
    {
        
        public IEnumerable<Category> RelatedCategories { get; set; }

        internal sealed class RecipeMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private RecipeMetadata()
            {
            }

            public EntityCollection<Category> Categories { get; set; }

            [Include]
            public EntityCollection<Comment> Comments { get; set; }

            public string CookingInstructions { get; set; }

            public string CreatedBy { get; set; }

            public DateTime DateCreated { get; set; }

            public Nullable<DateTime> DateModified { get; set; }

            public string Description { get; set; }

            [Include]
            public EntityCollection<Ingredient> Ingredients { get; set; }

            public string ModifiedBy { get; set; }

            public string Name { get; set; }

            public decimal RecipeId { get; set; }

            public TimeSpan TimeNeeded { get; set; }

            public byte[] Version { get; set; }
        }
    }
}