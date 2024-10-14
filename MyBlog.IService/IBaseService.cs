using System;
using MyBlog.IRepository;

namespace MyBlog.IService
{
	public interface IBaseService<TEntity> : IBaseRepository<TEntity> where TEntity: class, new()
	{

	}
}

