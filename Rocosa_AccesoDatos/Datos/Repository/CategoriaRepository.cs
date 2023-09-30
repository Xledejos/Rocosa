using Rocosa_AccesoDatos.Datos.Repository.IRepository;
using Rocosa_Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Rocosa_AccesoDatos.Datos.Repository
{
    public class CategoriaRepository : Repository<Categoria, int>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoriaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Categoria categoria)
        {
            var previousCategory = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);

            if (previousCategory != null)
            {
                previousCategory.NombreCategoria = categoria.NombreCategoria;
                previousCategory.MostrarOrden = categoria.MostrarOrden;
            }
        }
    }
}
