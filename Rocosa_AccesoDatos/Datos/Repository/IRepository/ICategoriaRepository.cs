using Rocosa_Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocosa_AccesoDatos.Datos.Repository.IRepository
{
    public interface ICategoriaRepository : IRepository<Categoria, int>
    {
        void Update(Categoria categoria);
    }
}
