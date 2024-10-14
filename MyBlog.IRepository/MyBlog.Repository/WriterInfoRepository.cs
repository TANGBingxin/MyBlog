using System;
using MyBlog.Model;
using MyBlog.IRepository;

namespace MyBlog.Repository
{
	public class WriterInfoRepository : BaseRepository<WriterInfo>, IWriterInfoRepository
	{
		public WriterInfoRepository()
		{
		}
	}
}

