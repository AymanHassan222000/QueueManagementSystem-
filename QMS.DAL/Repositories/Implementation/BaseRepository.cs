using Microsoft.EntityFrameworkCore;
using QMS.DAL.Context;
using QMS.DAL.Repositories.Interfaces;
using System.Linq.Expressions;

namespace QMS.DAL.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
	private readonly ApplicationDbContext _context;
	public BaseRepository(ApplicationDbContext context)
	{
		_context = context;
	}

    #region Functions Implementation
    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>>? criteria = null,
		string[]? includes = null,
		List<(Expression<Func<T,object>> KeySelector,bool ascending)>? orderBy = null
		)
	{
		IQueryable<T> query = _context.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				query = query.Include(include);

		if (orderBy is not null && orderBy.Any()) 
		{
			query = ApplyOrdering(query,orderBy);
        }

		if (criteria is not null)
			query = query.Where(criteria);

		return await query.ToListAsync();
	}

    public async Task<T?> GetByIdAsync(int id, string[]? includes = null)
	{
		IQueryable<T> query = _context.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				query = query.Include(include);

        var keyName = _context.Model
              .FindEntityType(typeof(T))!
              .FindPrimaryKey()!
              .Properties
              .First()
              .Name;

		return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, keyName) == id);
    }
	public T GetByIdOrDefault(int? id) 
	{
		if (id == null)
			return null;
		return _context.Set<T>().Find(id);
	}

	public async Task<T> AddAsync(T entity)
	{
		await _context.Set<T>().AddAsync(entity);
		return entity;
	}
	public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities) 
	{
		await _context.Set<T>().AddRangeAsync(entities);
		return entities;
	}
    public T Update(T entity)
	{
		_context.Update(entity);

		return entity;
	}
	public T Delete(T entity)
	{
		_context.Remove(entity);

		return entity;
	}
	public IEnumerable<T> DeleteRange(IEnumerable<T> entities) 
	{
		_context.Set<T>().RemoveRange(entities);
		return entities;
	}
	public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria, string[]? includes = null)
	{
		IQueryable<T> query = _context.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				query = query.Include(include);

		return await query.SingleOrDefaultAsync(criteria);
	}
	public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[]? includes = null) 
	{
		IQueryable<T> query = _context.Set<T>();

		if (includes is not null)
			foreach (var include in includes)
				query = query.Include(include);

		return await query.Where(criteria).ToListAsync();
	}

    public async Task<int> CountAsync() 
	{
		return await _context.Set<T>().CountAsync();
	}

	public async Task<int> CountAsync(Expression<Func<T, bool>> criteria) 
	{
		return await _context.Set<T>().CountAsync(criteria);
	}

	
	private IQueryable<T> ApplyOrdering(
		IQueryable<T> query,
		List<(Expression<Func<T,object>> KeySelector,bool ascending)> orderBy
	)
	{
		bool first = true;

		foreach (var (KeySelector, ascending) in orderBy) 
		{
			if (first)
			{
				query = ascending ? query.OrderBy(KeySelector) : query.OrderByDescending(KeySelector);
				first = false;
			}
			else 
			{
				query = ascending ? ((IOrderedQueryable<T>)query).ThenBy(KeySelector) : ((IOrderedQueryable<T>)query).ThenByDescending(KeySelector);
			}
		}

		return query;
	}


    #endregion
}
