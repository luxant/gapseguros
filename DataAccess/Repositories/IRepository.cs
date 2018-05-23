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

		void Create(T model);

		Task<T> GetById(int? id);

		void Update(T model);

		void DeleteById(int id);
	}
}
