using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DealerBlog.Entity;

namespace DealerBlog.Web.Web.Models
{
    public class PostVm
    {
            public IList<Post> Posts { get;  set; }
            public int TotalPosts { get; set; }
            public int PageSize { get; set; }
            public int PageNumer { get; set; }
        
    }
}