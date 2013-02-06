namespace RecipeBrowser.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;
    // The MetadataTypeAttribute identifies IngredientMetadata as the class
    // that carries additional metadata for the Ingredient class.
    [MetadataTypeAttribute(typeof(Ingredient.IngredientMetadata))]
    public partial class Ingredient
    {

        // This class allows you to attach custom attributes to properties
        // of the Ingredient class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class IngredientMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private IngredientMetadata()
            {
            }

            public decimal? Amount { get; set; }

            public string Description { get; set; }

            public decimal IngredientId { get; set; }

            public string Name { get; set; }

            public Recipe Recipe { get; set; }

            public decimal RecipeId { get; set; }

            [Include]
            public Unit Unit { get; set; }

            public decimal? UnitId { get; set; }

            public byte[] Version { get; set; }
        }
    }
}
