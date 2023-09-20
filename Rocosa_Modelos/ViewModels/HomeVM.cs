namespace Rocosa_Modelos.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Producto>? productos { get; set; }
        public IEnumerable<Categoria>? categorias { get; set; }
    }
}
