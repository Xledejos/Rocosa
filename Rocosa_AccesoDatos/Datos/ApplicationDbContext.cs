using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rocosa_Modelos;
using System.Security.Principal;

namespace Rocosa_AccesoDatos.Datos
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { 

        }

        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<TipoAplicacion>? TipoAplicaciones { set; get; } 
        public DbSet<Producto>? Productos { get; set; }
        public DbSet<UsuarioAplicacion>? UsuarioAplicacions { set; get; }
       
    }
}
