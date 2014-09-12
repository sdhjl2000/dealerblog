using System.Collections.Generic;
using System.Linq;
using Auto.Lib3.DataAccess;
using Autohome.CSH.Framework.Data;
using DealerBlog.Entity;
using LambdaSqlBuilder;
using LambdaSqlBuilder.ValueObjects;

namespace DealerBlog.DAL
{
    public partial class PostDAL
    {
        private static DatabaseProperty CONN = DBSettings.GetDatabaseProperty("Local");
 

        /// <summary>
        /// Return collection of posts based on pagination parameters.
        /// </summary>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> Posts(int pageNo, int pageSize)
        {

            SqlLamBase.SetAdapter(SqlAdapter.SqlServer2008);
            var subQuery = new SqlLam<Post>()
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn);

            var posts = CONN.SQLQuery<Post>(subQuery.QueryStringPage(pageSize, pageNo), subQuery.QueryParameters);
            return posts.ToList();

        }

        /// <summary>
        /// Return collection of posts belongs to a particular tag.
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            SqlLamBase.SetAdapter(SqlAdapter.SqlServer2008);

            var query = new SqlLam<Post>()
                .Select(o => o).Where(p => p.Published).OrderBy(x => x.PostedOn)
                .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
                .Join<Tag>((d, p) => d.Tag_id == p.Id).Where(t => t.UrlSlug == tagSlug);

            var posts = CONN.SQLQuery<Post>(query.QueryStringPage(pageSize, pageNo), query.QueryParameters);
            return posts.ToList();
          
        }

        /// <summary>
        /// Return collection of posts belongs to a particular category.
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            SqlLamBase.SetAdapter(SqlAdapter.SqlServer2008);

            var query = new SqlLam<Post>()
                .Select(o => o).Where(p => p.Published).OrderBy(x => x.PostedOn)
                .Join<Category>((o, d) => o.Category == d.Id).Where(t => t.UrlSlug == categorySlug);

            var posts = CONN.SQLQuery<Post>(query.QueryStringPage(pageSize, pageNo), query.QueryParameters);
            return posts.ToList();
        }

        /// <summary>
        /// Return collection of posts that matches the search text.
        /// </summary>
        /// <param name="search">search text</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var query = new SqlLam<Post>()
             .Select(o => o).Where(p => p.Published&& (p.Title.Contains(search))).OrderBy(x => x.PostedOn)
             .Join<Category>((o, d) => o.Category == d.Id).Where(t => t.UrlSlug.Contains(search))
             .Select(p => p);

            var posts = CONN.SQLQuery<Post>(query.QueryStringPage(pageSize, pageNo), query.QueryParameters);
            return posts.ToList();

         
        }

        /// <summary>
        /// Return total no. of all or published posts.
        /// </summary>
        /// <param name="checkIsPublished">True to count only published posts</param>
        /// <returns></returns>
        public int TotalPosts(bool checkIsPublished = true)
        {
            return CONN.Count<Post>(p => checkIsPublished || p.Published == true);
        }

        /// <summary>
        /// Return total no. of posts belongs to a particular category.
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <returns></returns>
        public int TotalPostsForCategory(string categorySlug)
        {
            var query = new SqlLam<Post>()
                .Where(p => p.Published)
                .Join<Category>((o, d) => o.Category == d.Id).Where(t => t.UrlSlug == categorySlug);


            return CONN.Count<Post>(query.QueryString, query.QueryParameters);
            
        }

        /// <summary>
        /// Return total no. of posts belongs to a particular tag.
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <returns></returns>
        public int TotalPostsForTag(string tagSlug)
        {
            var query = new SqlLam<Post>()
                .Where(p => p.Published)
                .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
                .Join<Tag>((d, p) => d.Tag_id == p.Id).Where(t => t.UrlSlug == tagSlug);

            
            return CONN.Count<Post>(query.QueryString, query.QueryParameters);
        }

        /// <summary>
        /// Return total no. of posts that matches the search text.
        /// </summary>
        /// <param name="search">search text</param>
        /// <returns></returns>
        public int TotalPostsForSearch(string search)
        {
            //return _session.Query<Post>()
            //               .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
            //               .Count();
            return 0;
        }

        /// <summary>
        /// Return posts based on pagination and sorting parameters.
        /// </summary>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortColumn">Sort column name</param>
        /// <param name="sortByAscending">True to sort by ascending</param>
        /// <returns></returns>
        public IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending)
        {
            SqlLamBase.SetAdapter(SqlAdapter.SqlServer2008);
         
            var query = new SqlLam<Post>();
            
            switch (sortColumn)
            {
                case "Title":
                    query = sortByAscending ? query.OrderBy(x => x.Title) : query.OrderByDescending(x => x.Title);
                    break;
                case "Published":
                    query = sortByAscending ? query.OrderBy(x => x.Published) : query.OrderByDescending(x => x.Published);
                    break;
                case "Modified":
                    query = sortByAscending ? query.OrderBy(x => x.Modified) : query.OrderByDescending(x => x.Modified);
                    break;
                default:
                    query = sortByAscending ? query.OrderBy(x => x.PostedOn) : query.OrderByDescending(x => x.PostedOn);
                  
                    break;
            }
            var posts = CONN.SQLQuery<Post>(query.QueryStringPage(pageSize, pageNo), query.QueryParameters);
            return posts.ToList();
            
        }

        /// <summary>
        /// Return post based on the published year, month and title slug.
        /// </summary>
        /// <param name="year">Published year</param>
        /// <param name="month">Published month</param>
        /// <param name="titleSlug">Post's url slug</param>
        /// <returns></returns>
        public Post Post(int year, int month, string titleSlug)
        {


            var query = new SqlLam<Post>()
                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug));

            return CONN.SQLQuery<Post>(query.QueryString, query.QueryParameters).FirstOrDefault();
        }

        /// <summary>
        /// Return post based on unique id.
        /// </summary>
        /// <param name="id">Post unique id</param>
        /// <returns></returns>
        public Post Post(int id)
        {
            return CONN.Get<Post>(id);
        }

        /// <summary>
        /// Adds a new post and returns the id.
        /// </summary>
        /// <param name="post"></param>
        /// <returns>Newly added post id</returns>
        public int AddPost(Post post)
        {
            CONN.Insert(post);
            return post.Id;
        }

        /// <summary>
        /// Update an existing post.
        /// </summary>
        /// <param name="post"></param>
        public void EditPost(Post post)
        {
            CONN.Update(post);
        }

        /// <summary>
        /// Delete the post permanently from database.
        /// </summary>
        /// <param name="id"></param>
        public void DeletePost(int id)
        {
            CONN.Delete<Post>(x => x.Id == id);
        }

    }
}