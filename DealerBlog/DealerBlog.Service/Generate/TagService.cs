
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
    public partial class TagService :ITag
    {
         
        public bool Add(Tag entity)
        {
            return TagDAL.Instance.Add(entity);
        }
        
         /// <summary>
         /// 从数据库中删除指定的实体，是以主键方式删除
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>删除成功则返回true</returns>
         public bool Delete(Tag entity)
         {
            return TagDAL.Instance.Delete(entity);
         }
         
        /// <summary>
        /// 根据lambda删除行
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<Tag, bool>> expression)
        {
            return TagDAL.Instance.Delete(expression);
        }
         
         /// <summary>
         /// 更新数据行
         /// </summary>
         /// <param name="entity">实体</param>
         /// <returns>更新成功则返回true</returns>
         public bool Update(Tag entity)
         {
            return TagDAL.Instance.Update(entity);
         }
         
         //// <summary>
         /// 根据主键返回实体
         /// </summary>
         /// <param name="id">主键值</param>
         /// <returns>Tag</returns>/
         public Tag Get(object id)
         {
            return TagDAL.Instance.Get(id);
         }
         
        /// <summary>
        /// 根据sql条件获取记录
        /// </summary>
        /// <param name="strWhere">where语句</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
         public IList<Tag> Find(string strWhere = null, object param = null)
         {
            return TagDAL.Instance.Find(strWhere, param).ToList();            
         }
         
        /// <summary>
        /// 根据lambda表达式查询表
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IList<Tag> Find(Expression<Func<Tag, bool>> expression)
        {
            return TagDAL.Instance.Find(expression).ToList();
        }
    }
}