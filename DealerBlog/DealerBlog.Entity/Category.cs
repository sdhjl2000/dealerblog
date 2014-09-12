using System.Collections.Generic;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
    [Table("Category")]
    public partial class Category
    {
        public virtual IList<Post> Posts { get; set; }
    }
}