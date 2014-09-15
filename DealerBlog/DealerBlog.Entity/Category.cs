using System.Collections.Generic;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Posts=new List<Post>();
        }

        public virtual IList<Post> Posts { get; set; }
    }
}