using System;
using SqlSugar;
namespace MyBlog.Model
{
	public class BlogNews : BaseId
	{
		//nvarchar可以带中文
		[SugarColumn(ColumnDataType = "nvarchar(30)")]
		public string Title { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(max)")]
        public string Content { get; set; }

		public DateTime Time { get; set; }
		public int BrowseCount { get; set; }
		public int LikeCount { get; set; }

        public int TypeId { get; set; }
		public int WriterId { get; set; }

		//类型ORM不映射到数据库
		[SugarColumn(IsIgnore = true)]
		public TypeInfo TypeInfo { get; set; }
        [SugarColumn(IsIgnore = true)]
        public WriterInfo WriterInfo { get; set; }
    }
}

