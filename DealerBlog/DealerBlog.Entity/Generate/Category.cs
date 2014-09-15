using System;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
	/// <summary>
	///Category数据实体
	///本类代码由代码生成器自动生成不要轻易修改
	/// </summary>
	[Serializable]
	public partial class Category :BaseEntity
	{
		#region 属性
		///<summary>
		///
		///</summary>        
        [Key]
        [Identity]
		public int CategoryId { get; set; }
		///<summary>
		///
		///</summary>        
		public string Name { get; set; }
		///<summary>
		///
		///</summary>        
		public string CatUrlSlug { get; set; }
		///<summary>
		///
		///</summary>        
		public string CatDescription { get; set; }
        #endregion
        
	}
}
