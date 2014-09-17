using System;
using NUnit.Framework;

namespace DealerBlogTest.Dal
{
    [TestFixture]
    public class PostDalTest
    {
        [Test]
        public void test_post_pagesize()
        {
            var list = DealerBlog.DAL.PostDAL.Instance.Posts(1, 100);
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

        [Test]
        public void test_post_forsearch()
        {
            var str = "i";
            var count = DealerBlog.DAL.PostDAL.Instance.TotalPostsForSearch(str);
            Console.WriteLine(count);

            var list = DealerBlog.DAL.PostDAL.Instance.PostsForSearch(str, 1, 100);
            Console.WriteLine(list.Count);
        }
    }

}
