using System.Linq.Expressions;

namespace QMS.DAL.Repositories.Interfaces;

public interface IBaseRepository<T> where T : class
{
	Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? criteria = null,
		string[]? includes = null,
		List<(Expression<Func<T, object>> KeySelector, bool ascending)>? orderBy = null);

    Task<T?> GetByIdAsync(int id,string[]? includes = null);
    
	Task<T> AddAsync(T entity);
	Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
	T Update(T entity);
	T Delete(T entity);
	IEnumerable<T> DeleteRange(IEnumerable<T> entities);
	Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes);
	Task<IEnumerable<T>> FindAllAsync(Expression<Func<T,bool>> criteria, string[]? includes);

    Task<int> CountAsync();
	Task<int> CountAsync(Expression<Func<T,bool>> criteria);
}
