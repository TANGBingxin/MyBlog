using SqlSugar;
using MyBlog.Model;
using MyBlog.IRepository;
using System.Linq.Expressions;

namespace MyBlog.Repository
{
	public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
	{
        public override async Task<List<BlogNews>> QueryAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(w => w.WriterInfo,w=>w.WriterId, w=>w.WriterInfo.Id)
                .ToListAsync();
        }

        public override async Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                            .Where(func)
                            .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                            .Mapper(w => w.WriterInfo, w => w.WriterId, w => w.WriterInfo.Id)
                            .ToListAsync();
        }

        public override Task<List<BlogNews>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return base.Context.Queryable<BlogNews>()
                .ToPageListAsync(page, size, total);
        }

        public override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func, int page, int size, RefAsync<int> total)
        {
            return base.Context.Queryable<BlogNews>()
                .Where(func)
                .ToPageListAsync(page, size, total);
        }
    }
}

