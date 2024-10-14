using System;
using MyBlog.Model;

namespace MyBlog.IRepository
{
	public interface IWriterInfoRepository : IBaseRepository<WriterInfo>
    {
		//加上writer独有的方法
	}
}

