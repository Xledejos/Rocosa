using System.ComponentModel.DataAnnotations;

namespace Rocosa_Modelos
{
    public class TipoAplicacion : BaseEntity<int>
    {
        [Required(ErrorMessage ="Obligatorio")]
        public string NombreTipAplicacion { get; set; }
    }
}
