using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Autohome.CSH.Framework.Data;
using Auto.Lib3.DataAccess;

namespace DealerBlog.DAL
{
    
	public static partial class DbConnectonExtensions
    {
        /// <summary>
        /// 创建读连接
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static IDbConnection GetReaderConnection(this DatabaseProperty dp)
        {
            if (dp != null)
            {
                var conn = new SqlConnection(dp.Reader.ConnectionString);
                conn.Open();
                return conn;
            }
            throw new Exception("数据库读连接创建失败");
        }

        /// <summary>
        /// 创建写连接
        /// </summary>
        /// <param name="dp"></param>
        /// <returns></returns>
        public static IDbConnection GetWriterConnection(this DatabaseProperty dp)
        {
            if (dp != null)
            {
                var conn = new SqlConnection(dp.Reader.ConnectionString);
                conn.Open();
                return conn;
            }
            throw new Exception("数据库写连接创建失败");
        }
    }
}
		