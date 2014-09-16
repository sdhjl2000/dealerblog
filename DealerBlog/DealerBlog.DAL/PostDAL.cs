using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        /// 查询所有的post，同时填充对应的category和tags
        /// </summary>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> Posts(int pageNo, int pageSize)
        {

            var subQuery = new SqlLam<Post>()
                .Where(p => p.Published)
                .OrderByDescending(p => p.PostedOn)
                .Select(p => p)
                .Join<Category>((p, c) => p.Category == c.CategoryId)
                .Select(t => t);


            var posts = CONN.SQLQuery<Post, Category, Post>(subQuery.QueryStringPage(pageSize, pageNo),
                (p, c) => { p.BlogCategory = c; p.Tags = GetTags(p); return p; }, subQuery.QueryParameters, splitOn: "CategoryId");
            return posts.ToList();


            //var subQuery = new SqlLam<Category>()
            //    .Select(c => c).Join<Post>((c, p) => c.CategoryId == p.Category)
            //    .Where(p => p.Published)
            //    .OrderByDescending(p => p.PostedOn)
            //    .Select(p => p);

            

            //var posts = CONN.SQLQuery<Category, Post, Post>(subQuery.QueryStringPage(pageSize, pageNo),
            //    (c, p) =>
            //    {
            //        p.BlogCategory = c;
            //        p.Tags=GetTags(p);
            //        return p;
            //    }, subQuery.QueryParameters, splitOn: "Id");
            //return posts.ToList();

        }
        /// <summary>
        /// 对于多对多的关联,需要进行二次查询获取对应的数据
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public List<Tag> GetTags(Post p)
        {
            var subQuery = new SqlLam<PostTagMap>().Where(x=>x.Post_id==p.Id).SelectDistinct(x=>x.Tag_id);
            var query = new SqlLam<Tag>().WhereIsIn(x => x.TagId, subQuery);
            return CONN.SQLQuery<Tag>(query.QueryString, query.QueryParameters).ToList();
        }
        /// <summary>
        /// 单词查询获取对应的类目
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Category GetCategory(Post p)
        {
            
            return CONN.Where<Category>(x=>x.CategoryId==p.Category).First();
        }

        /// <summary>
        /// 根据tag查询post，查询语句为针对多对多的
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {

            var query = new SqlLam<Post>()
                .Select(o => o).Where(p => p.Published).OrderBy(x => x.PostedOn)
                .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
                .Join<Tag>((d, p) => d.Tag_id == p.TagId).Where(t => t.TagUrlSlug == tagSlug).Select(t=>t);
            

            var posts = CONN.SQLQuery<Post, Tag, Post>(query.QueryStringPage(pageSize, pageNo), (p, t) =>
            { p.BlogCategory = GetCategory(p); p.Tags.Add(t); return p; }, query.QueryParameters, splitOn: "TagId");
            return posts.ToList();

        }

        /// <summary>
        /// 根据类目查询post,语句为多对一的查询
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {

            var query = new SqlLam<Post>()
                .Select(o => o).Where(p => p.Published).OrderBy(x => x.PostedOn)
                .Join<Category>((o, d) => o.Category == d.CategoryId).Where(t => t.CatUrlSlug == categorySlug).Select(c=>c);

            var posts = CONN.SQLQuery<Post,Category,Post>(query.QueryStringPage(pageSize, pageNo), (p, c) =>
            {
                p.BlogCategory = c;
                p.Tags = GetTags(p);
                return p;
            }, query.QueryParameters,splitOn:"CategoryId");
            return posts.ToList();
        }

        /// <summary>
        /// 根据关键词查询，构建的语句为多级关联查询，首先子查询语句获取主键列表，然后进行in查询
        /// </summary>
        /// <param name="search">search text</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var subquery = new SqlLam<Category>().Or(t => t.CatUrlSlug.Contains(search))
                .Join<Post>((c, p) =>  c.CategoryId==p.Category )
                .Where(p => p.Published).Or(p => p.Title.Contains(search)).SelectDistinct(p=>p.Id)
                .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
                .Join<Tag>((d, p) => d.Tag_id == p.TagId).Or(t => t.TagUrlSlug == search);
            var query = new SqlLam<Post>().WhereIsIn(p => p.Id, subquery).OrderBy(x => x.PostedOn);//TODO：效率存在问题



            var posts = CONN.SQLQuery<Post>(query.QueryStringPage(pageSize, pageNo), query.QueryParameters).ToList();
            posts.ForEach((x) => { x.BlogCategory = GetCategory(x);
                                                     x.Tags = GetTags(x);
            });
            return posts;

        }
        /// <summary>
        /// 获取关键词查询的总数
        /// </summary>
        /// <param name="search">search text</param>
        /// <returns></returns>
        public int TotalPostsForSearch(string search)
        {
            //TODO:生成的or语句查询优先级不正确
            var subquery = new SqlLam<Category>().Or(t => t.CatUrlSlug.Contains(search))
               .Join<Post>((c, p) => c.CategoryId == p.Category)
               .Where(p => p.Published).Or(p => p.Title.Contains(search)).SelectCount(p => p.Id)
               .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
               .Join<Tag>((d, p) => d.Tag_id == p.TagId).Or(t => t.TagUrlSlug == search);

            return CONN.Count<Post>(subquery.QueryString, subquery.QueryParameters);
        }

        /// <summary>
        /// 获取关查询的总数
        /// </summary>
        /// <param name="checkIsPublished">True to count only published posts</param>
        /// <returns></returns>
        public int TotalPosts(bool checkIsPublished = true)
        {
            return CONN.Count<Post>(p => p.Published == checkIsPublished);
        }

        /// <summary>
        /// 获取类目查询的总数
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <returns></returns>
        public int TotalPostsForCategory(string categorySlug)
        {
            var query = new SqlLam<Post>()
                .Where(p => p.Published)
                .Join<Category>((o, d) => o.Category == d.CategoryId).Where(t => t.CatUrlSlug == categorySlug);

            //return 10;
            return CONN.Count<Post>(query.QueryString, query.QueryParameters);

        }

        /// <summary>
        /// 获取tag查询的总数
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <returns></returns>
        public int TotalPostsForTag(string tagSlug)
        {
            var query = new SqlLam<Post>()
                .Where(p => p.Published)
                .Join<PostTagMap>((o, d) => o.Id == d.Post_id)
                .Join<Tag>((d, p) => d.Tag_id == p.TagId).Where(t => t.TagUrlSlug == tagSlug);

            //return 10;
            return CONN.Count<Post>(query.QueryString, query.QueryParameters);
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