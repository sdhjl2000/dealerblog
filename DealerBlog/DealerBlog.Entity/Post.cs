using System.Collections.Generic;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
    [Table("Post")]
    public partial class Post
    {
        public virtual Category BlogCategory { get; set; }

        public virtual IList<Tag> Tags
        { get; set; }
    }
}