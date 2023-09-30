using System.ComponentModel.DataAnnotations;

namespace Rocosa_Modelos
{
    public class Categoria : BaseEntity<int>
    {
        [Required(ErrorMessage ="Obligatorio")]
        public string NombreCategoria { get; set; }
        [Required(ErrorMessage ="Obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage ="Debe ser mayor a cero")]
        public int MostrarOrden { get; set; }
    }
}
