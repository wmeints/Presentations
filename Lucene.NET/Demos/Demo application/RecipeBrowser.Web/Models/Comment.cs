using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RecipeBrowser.Web.Models
{
    [MetadataTypeAttribute(typeof(Comment.CommentMetadata))]
    public partial class Comment : ITrackableEntity
    {
        internal sealed class CommentMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private CommentMetadata()
            {
            }

            public decimal CommentId { get; set; }

            public string CommentText { get; set; }

            public string CreatedBy { get; set; }

            public DateTime DateCreated { get; set; }

            public Nullable<DateTime> DateModified { get; set; }

            public string ModifiedBy { get; set; }

            public Recipe Recipe { get; set; }

            public decimal RecipeId { get; set; }

            public byte[] Version { get; set; }
        }
    }
}
