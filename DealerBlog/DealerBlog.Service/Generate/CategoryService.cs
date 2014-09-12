
using System;
using System.Collections.Generic;
using System.Text;
using DealerBlog.Entity;
using DealerBlog.DAL;
using DealerBlog.BLL;
using System.Linq;
using System.Linq.Expressions;


namespace DealerBlog.Service
{
    public partial class CategoryService :ICategory
    {
         
        public bool Add(Category entity)
        {
            return CategoryDAL.Instance.Add(entity);
        }
        
         /// <summary>
         /// 从数据库中删除指定的实体，是以主键方式删除
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>删除成功则返回true</returns>
         public bool Delete(Category entity)
         {
            return CategoryDAL.Instance.Delete(entity);
         }
         
        /// <summary>
        /// 根据lambda删除行
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<Category, bool>> expression)
        {
            return CategoryDAL.Instance.Delete(expression);
        }
         
         /// <summary>
         /// 更新数据行
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>更新成功则返回true</returns>
         public bool Update(Category entity)
         {
            return CategoryDAL.Instance.Update(entity);
         }
         
         //// <summary>
         /// 根据主键返回实体
         /// </summary>
         /// <param name="id">主键值</param>
         /// <returns>Category</returns>/
         public Category Get(object id)
         {
            return CategoryDAL.Instance.Get(id);
         }
         
        /// <summary>
        /// 根据sql条件获取记录
        /// </summary>
        /// <param name="strWhere">where语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
         public IList<Category> Find(string strWhere = null, object param = null)
         {
            return CategoryDAL.Instance.Find(strWhere, param).ToList();            
         }
         
        /// <summary>
        /// 根据lambda表达式查询表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IList<Category> Find(Expression<Func<Category, bool>> expression)
        {
            return CategoryDAL.Instance.Find(expression).ToList();
        }
    }
}