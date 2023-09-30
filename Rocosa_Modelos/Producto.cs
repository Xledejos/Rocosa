using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rocosa_Modelos
{
    public class Producto : BaseEntity<int>
    {
        [Required(ErrorMessage ="Obligatorio")]
        public string NombreProducto { get; set; }
        [Required(ErrorMessage ="Obligatorio")]
        public string DescripcionCorta { get; set; }
        [Required(ErrorMessage ="Obligatorio")]
        public string DescripcionProducto { get; set; }
        [Required(ErrorMessage ="Obligatorio")]
        [Range(1,1000, ErrorMessage ="Entre 0 y 1001")]
        public double Precio { get; set; }
        public string ImagenUrl { get; set; }

        // Foreign Key
        public int CategoriaId { get; set; }
        [ForeignKey(nameof(CategoriaId))]
        public virtual Categoria Categoria { get; set; }
        public virtual IList<Categoria> Categorias { get; } = new List<Categoria>();
        public int TipoAplicacionId { get; set; }
        [ForeignKey("TipoAplicacionId")]
        public virtual TipoAplicacion TipoAplicacion { get; set; }
    }
}
