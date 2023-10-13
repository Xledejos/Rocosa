using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rocosa_AccesoDatos.Datos;
using Rocosa_Modelos;
using Rocosa_Modelos.ViewModels;
using Rocosa_Utilidades;
using System.Diagnostics;

namespace Rocosa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new ()
            {
                Productos = _db.Productos.Include(c=>c.Categoria).Include(t=>t.TipoAplicacion),
                Categorias = _db.Categorias
            };
            return View(homeVM);
        }

        // GET
        public IActionResult Detalle(int id)
        {
            List<CarroCompra> carroCompraList = new();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraList = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            DetalleVM detalleVM = new()
            {
                Producto = _db.Productos.Include(c => c.Categoria).Include(t => t.TipoAplicacion).Where(p=>p.Id==id).FirstOrDefault(),
				ExiteEnCarro = false
            };

            foreach (var item in carroCompraList)
            {
                if (item.ProductoId == id)
                {
                    detalleVM.ExiteEnCarro = true;
                }
            }

            return View(detalleVM);
        }

        // Post - Agregar al Carro
        [HttpPost, ActionName("Detalle")]
        public IActionResult DetallePost(int id)
        {
            List<CarroCompra> carroCompraList = new();
            if(HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraList = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }
            carroCompraList.Add(new CarroCompra { ProductoId = id });
            HttpContext.Session.Set(WC.SessionCarroCompras, carroCompraList);

            return RedirectToAction("Index");
        }

        // Get método de acción - Remover del Carro
        public IActionResult RemoverDeCarro(int id)
        {
            List<CarroCompra> carroCompraList = new();
            if (HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras) != null &&
                HttpContext.Session.Get<IEnumerable<CarroCompra>>(WC.SessionCarroCompras).Count() > 0)
            {
                carroCompraList = HttpContext.Session.Get<List<CarroCompra>>(WC.SessionCarroCompras);
            }

            var productoARemover = carroCompraList.SingleOrDefault(p => p.ProductoId == id);

            if (productoARemover != null)
            {
                carroCompraList.Remove(productoARemover);
            }

            HttpContext.Session.Set(WC.SessionCarroCompras, carroCompraList);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }
         
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}