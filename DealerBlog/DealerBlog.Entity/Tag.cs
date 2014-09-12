using System.Collections.Generic;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
    [Table("Tag")]
    public partial class Tag
    {
        public virtual IList<Post> Posts
        { get; set; }
    }
}