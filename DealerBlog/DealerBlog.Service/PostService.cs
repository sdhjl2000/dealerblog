
using System.Collections.Generic;
using DealerBlog.Entity;

namespace DealerBlog.Service
{
    
    public partial class PostService
    {
        public IList<Post> Posts(int pageNo, int pageSize)
        {
          return  DealerBlog.DAL.PostDAL.Instance.Posts(pageNo, pageSize);
        }

        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
           return DealerBlog.DAL.PostDAL.Instance.PostsForTag(tagSlug,pageNo, pageSize);
        }

        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            return DealerBlog.DAL.PostDAL.Instance.PostsForCategory(categorySlug,pageNo, pageSize);
        }

        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            return DealerBlog.DAL.PostDAL.Instance.PostsForSearch(search, pageNo, pageSize);

        }

        public int TotalPosts(bool checkIsPublished = true)
        {
            return DealerBlog.DAL.PostDAL.Instance.TotalPosts(checkIsPublished);
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return DealerBlog.DAL.PostDAL.Instance.TotalPostsForCategory(categorySlug);
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return DealerBlog.DAL.PostDAL.Instance.TotalPostsForTag(tagSlug);
        }

        public int TotalPostsForSearch(string search)
        {
            return DealerBlog.DAL.PostDAL.Instance.TotalPostsForSearch(search);
        }

        public IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            return DealerBlog.DAL.PostDAL.Instance.Posts(pageNo,pageSize,sortColumn,sortByAscending);

        }

        public Post Post(int year, int month, string titleSlug)
        {
            return DealerBlog.DAL.PostDAL.Instance.Post(year,month,titleSlug);
        }
    }
}