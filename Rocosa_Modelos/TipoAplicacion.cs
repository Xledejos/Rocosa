using System.ComponentModel.DataAnnotations;

namespace Rocosa_Modelos
{
    public class TipoAplicacion
    {
        [Key] 
        public int Id { get; set; }
        [Required(ErrorMessage ="Obligatorio")]
        public string NombreTipAplicacion { get; set; }
    }
}
