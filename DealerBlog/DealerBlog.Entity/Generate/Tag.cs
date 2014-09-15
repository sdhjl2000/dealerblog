﻿using System;
using Autohome.CSH.Framework;

namespace DealerBlog.Entity
{
	/// <summary>
	///Tag数据实体
	///本类代码由代码生成器自动生成不要轻易修改
	/// </summary>
	[Serializable]
	public partial class Tag :BaseEntity
	{
		#region 属性
		///<summary>
		///
		///</summary>        
        [Key]
        [Identity]
		public int TagId { get; set; }
		///<summary>
		///
		///</summary>        
		public string TagName { get; set; }
		///<summary>
		///
		///</summary>        
		public string TagUrlSlug { get; set; }
		///<summary>
		///
		///</summary>        
		public string TagDescription { get; set; }
        #endregion
        
	}
}
