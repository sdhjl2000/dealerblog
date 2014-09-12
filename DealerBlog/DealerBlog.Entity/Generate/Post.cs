using System;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
	/// <summary>
	///Post数据实体
	///本类代码由代码生成器自动生成不要轻易修改
	/// </summary>
	[Serializable]
	public partial class Post :BaseEntity
	{
		#region 属性
		///<summary>
		///
		///</summary>        
        [Key]
        [Identity]
		public int Id { get; set; }
		///<summary>
		///
		///</summary>        
		public string Title { get; set; }
		///<summary>
		///
		///</summary>        
		public string ShortDescription { get; set; }
		///<summary>
		///
		///</summary>        
		public string Description { get; set; }
		///<summary>
		///
		///</summary>        
		public string Meta { get; set; }
		///<summary>
		///
		///</summary>        
		public string UrlSlug { get; set; }
		///<summary>
		///
		///</summary>        
		public bool Published { get; set; }
		///<summary>
		///
		///</summary>        
		public DateTime PostedOn { get; set; }
		///<summary>
		///
		///</summary>        
		public DateTime? Modified { get; set; }
		///<summary>
		///
		///</summary>        
		public int Category { get; set; }
        #endregion
        
	}
}
