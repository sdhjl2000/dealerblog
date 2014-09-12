using System;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
	/// <summary>
	///PostTagMap数据实体
	///本类代码由代码生成器自动生成不要轻易修改
	/// </summary>
	[Serializable]
	public partial class PostTagMap :BaseEntity
	{
		#region 属性
		///<summary>
		///
		///</summary>        
		public int Post_id { get; set; }
		///<summary>
		///
		///</summary>        
		public int Tag_id { get; set; }
        #endregion
        
	}
}
