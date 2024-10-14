using System;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;

namespace MyBlog.Service
{
	public class BlogNewsService:BaseService<BlogNews>,IBlogNewsService
	{
		private readonly IBlogNewsRepository _iBlogNewsRepository;

        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository)
		{
			_iBlogNewsRepository = iBlogNewsRepository;
            base._iBaseRepository = iBlogNewsRepository;

        }
	}
}

