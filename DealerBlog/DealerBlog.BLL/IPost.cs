
using System;
using System.Collections.Generic;
using System.Text;
using DealerBlog.Entity;
using Autohome.CSH.Framework;

namespace DealerBlog.BLL
{
    public  interface IPost:IBaseBLL<Post>
    {
        /// <summary>
        /// Return collection of posts based on pagination parameters.
        /// </summary>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        IList<Post> Posts(int pageNo, int pageSize);

        /// <summary>
        /// Return collection of posts belongs to a particular tag.
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);

        /// <summary>
        /// Return collection of posts belongs to a particular category.
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);

        /// <summary>
        /// Return collection of posts that matches the search text.
        /// </summary>
        /// <param name="search">search text</param>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns></returns>
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);

        /// <summary>
        /// Return total no. of all or published posts.
        /// </summary>
        /// <param name="checkIsPublished">True to count only published posts</param>
        /// <returns></returns>
        int TotalPosts(bool checkIsPublished = true);

        /// <summary>
        /// Return total no. of posts belongs to a particular category.
        /// </summary>
        /// <param name="categorySlug">Category's url slug</param>
        /// <returns></returns>
        int TotalPostsForCategory(string categorySlug);

        /// <summary>
        /// Return total no. of posts belongs to a particular tag.
        /// </summary>
        /// <param name="tagSlug">Tag's url slug</param>
        /// <returns></returns>
        int TotalPostsForTag(string tagSlug);

        /// <summary>
        /// Return total no. of posts that matches the search text.
        /// </summary>
        /// <param name="search">search text</param>
        /// <returns></returns>
        int TotalPostsForSearch(string search);

        /// <summary>
        /// Return posts based on pagination and sorting parameters.
        /// </summary>
        /// <param name="pageNo">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="sortColumn">Sort column name</param>
        /// <param name="sortByAscending">True to sort by ascending</param>
        /// <returns></returns>
        IList<Post> Posts(int pageNo, int pageSize, string sortColumn, bool sortByAscending);

        /// <summary>
        /// Return post based on the published year, month and title slug.
        /// </summary>
        /// <param name="year">Published year</param>
        /// <param name="month">Published month</param>
        /// <param name="titleSlug">Post's url slug</param>
        /// <returns></returns>
        Post Post(int year, int month, string titleSlug);

    }
}