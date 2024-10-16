using Microsoft.EntityFrameworkCore;
using QMS.BL.Interfaces;
using QMS.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QMS.DAL.Repositories
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		protected ApplicationDbContext _context;
		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await _context.Set<T>().ToListAsync();
		}

		public async Task<T> GetById(long id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
		public async Task<T> Add(T entity)
		{
			await _context.Set<T>().AddAsync(entity);
			_context.SaveChanges();

			return entity;
		}
		public T Update(T entity)
		{
			_context.Update(entity);
			_context.SaveChanges();

			return entity;
		}

		public T Delete(T entity)
		{
			_context.Remove(entity);
			_context.SaveChanges();

			return entity;
		}

		public async Task<T> Find(Expression<Func<T, bool>> match)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(match);
		}

	}
}
