using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Rocosa_AccesoDatos.Datos.Repository.IRepository;
using Rocosa_Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rocosa_AccesoDatos.Datos.Repository
{
    public abstract class Repository<T, TId> : IRepository<T, TId>
        where T : BaseEntity<TId>
        where TId : IEquatable<TId>
    {
        private readonly ApplicationDbContext _context;
        protected DbSet<T> Entities => _context.Set<T>();

        protected Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<T?> GetById(TId id)
            => await Entities.FirstOrDefaultAsync(i => i.Id.Equals(id));


        public T GetFirst(Expression<Func<T, bool>>? filtro = null, string? includeProperty = null, bool isTracking = true)
        {
            IQueryable<T> query = Entities;

            if (filtro != null)
            {
                query = query.Where(filtro);
            }

            if (includeProperty != null)
            {
                foreach (var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item); // Categoria, TipoAplicacion
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return query.FirstOrDefault();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperty = null, bool isTracking = true)
        {
            IQueryable<T> query = Entities;

            if(filtro != null)
            {
                query = query.Where(filtro);
            }

            if(includeProperty != null)
            {
                foreach (var item in includeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item); // Categoria, TipoAplicacion
                }
            }

            if(orderBy != null)
            {
                query = orderBy(query);
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public void Insert(T entity)
        {
            Entities.Add(entity);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}

