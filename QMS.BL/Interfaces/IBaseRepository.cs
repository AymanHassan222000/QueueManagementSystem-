using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace QMS.BL.Interfaces
{
	public interface IBaseRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(long id);
		Task<T> Add(T entity);
		T Update(T entity);
		T Delete(T entity);
		Task<T> Find(Expression<Func<T, bool>> match);
	}
}
