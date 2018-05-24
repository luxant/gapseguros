using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public interface IRepository<T>
	{
		Task<IQueryable<T>> GetAll();

		Task<T> Create(T model);

		Task<T> GetById(int? id);

		Task Update(T model);

		Task DeleteById(int id);
	}
}
