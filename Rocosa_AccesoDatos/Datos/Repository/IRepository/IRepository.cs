using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocosa_AccesoDatos.Datos.Repository.IRepository
{
    public interface IRepository<T, TId> 
    {
        Task<T?> GetById(TId id);
        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string includeProperty = null,
            bool isTracking = true
            );
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperty = null,
            bool isTracking = true
            );
        void Insert(T entity);
        void Delete(T entity);
        void Save();

    }
}
