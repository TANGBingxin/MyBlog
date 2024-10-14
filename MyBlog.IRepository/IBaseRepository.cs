using System.Linq.Expressions;
using SqlSugar;

namespace MyBlog.IRepository
{
	/// <summary>
	/// 所有Table的CRUD都可以使用，用泛型，并约束为class
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public interface IBaseRepository<TEntity> where TEntity : class, new()
	{
		Task<bool> CreateAsync(TEntity entity);
		Task<bool> DeleteAsync(int id);
		Task<bool> EditAsync(TEntity entity);
		Task<TEntity> FindAsync(int id);
		Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

		//查询所有数据
		Task<List<TEntity>> QueryAsync();

		//自定义查询
		Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);

		//分页查询 因为不确定自定义的条件是什么，但规定为一个func及其参数
		Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);

		//自定义条件分页查询
		Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);
	}
	
}

