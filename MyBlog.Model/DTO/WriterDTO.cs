using System;
namespace MyBlog.Model.DTO
{
    /// <summary>
    /// 将Writer类映射到WriterDTO方便输出
    /// </summary>
	public class WriterDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

    }
}

