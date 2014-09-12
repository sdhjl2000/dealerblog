

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using DealerBlog.Entity;
using Autohome.CSH.Framework.Data;
using System.Linq.Expressions;


namespace DealerBlog.DAL
{
    /// <summary>
    /// 数据层实例化接口类 dbo.Post
    /// </summary>
    public partial class PostDAL
    {
		public static PostDAL Instance = new PostDAL();

        private  PostDAL(){}
        
        /// <summary>
        /// 在数据库中添加行，如果主键是自增的那么设置主键是没有意义的
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool Add(Post entity)
        {
            return CONN.Insert<Post>(entity);            
        }
        
         /// <summary>
         /// 从数据库中删除指定的实体，是以主键方式删除
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>删除成功则返回true</returns>
         public bool Delete(Post entity)
         {         
            return CONN.Delete<Post>(entity);
         }
         
        /// <summary>
        /// 通过lambda表达式删除行
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<Post, bool>> expression)
        {
            return CONN.Delete<Post>(expression);
        }
         
         /// <summary>
         /// 更新数据行
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>更新成功则返回true</returns>
         public bool Update(Post entity)
         {
            return CONN.Update<Post>(entity);            
         }
         
         //// <summary>
         /// 根据主键返回实体
         /// </summary>
         /// <param name="id">主键值</param>
         /// <returns>Post</returns>/
         public Post Get(object id)
         {
            return CONN.Get<Post>(id);
            
         }
         
         /// <summary>
        /// 根据sql条件获取记录
        /// </summary>
        /// <param name="strWhere">where语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
         public IEnumerable<Post> Find(string strWhere = null, object param = null)
         {
            return CONN.SQLQueryWhere<Post>(strWhere, param);        
         }
         
         /// <summary>
        /// 通过lambda查找表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
         public IEnumerable<Post> Find(Expression<Func<Post, bool>> expression)
         {
             return CONN.Where<Post>(expression).ToList();
         } 
	}
}
		