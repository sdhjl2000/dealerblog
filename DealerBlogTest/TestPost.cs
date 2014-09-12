using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
namespace DealerBlogTest
{
    [TestFixture]
    public class TestPost
    {
        [Test]
        public void test_post_pagesize()
        {
            var list= DealerBlog.DAL.PostDAL.Instance.Posts(1,100);
            foreach (var p in list)
            {
                Console.WriteLine(p.Title);
            }
        }

        [Test]
        public void test_post_postorder()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.Posts(1, 100, "Title", true);
            foreach (var p in list)
            {
                Console.WriteLine(p.Title);
            }
        }

        [Test]
        public void test_post_forcag()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.PostsForCategory("programming", 1, 100);
            foreach (var p in list)
            {
                Console.WriteLine(p.Title);
            }
        }

        [Test]
        public void test_post_fortat()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.PostsForTag("asp", 1, 100);
            foreach (var p in list)
            {
                Console.WriteLine(p.Title);
            }
        }


        [Test]
        public void test_post_totalfortag()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.TotalPostsForTag("asp");
            Console.WriteLine(list);
        }

        [Test]
        public void test_post_totalforcat()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.TotalPostsForCategory("programming");
            Console.WriteLine(list);
        }
    }
}
