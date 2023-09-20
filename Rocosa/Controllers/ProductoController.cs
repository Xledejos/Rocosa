using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Rocosa_AccesoDatos.Datos;
using Rocosa_Modelos;
using Rocosa_Modelos.ViewModels;
using Rocosa_Utilidades;

namespace Rocosa.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Producto> lista = _db.Productos.Include(c => c.Categoria).Include(t  => t.TipoAplicacion);

            return View(lista);
        }

        // GET UPSERT: CREAR O EDITAR
        public  IActionResult Upsert(int? id)
        {
            ProductoVM productoVM = new ProductoVM()
            {
                Producto = new Producto(),

                CategoriaLista = _db.Categorias.Select(c => new SelectListItem
                {
                    Text = c.NombreCategoria,
                    Value = c.Id.ToString()
                }),

                TipoAplicacionList = _db.TipoAplicaciones.Select(t => new SelectListItem
                {
                    Text = t.NombreTipAplicacion,
                    Value = t.Id.ToString()
                })
            };

            if(id == null)
            {
                // Crear nuevo producto
                return View(productoVM);
            }
            else
            {
                // Editar producto
                productoVM.Producto = _db.Productos.Find(id);

                if(productoVM == null)
                {
                    return NotFound();
                }
                return View(productoVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductoVM productoVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if(productoVM.Producto.Id == 0)
                {
                    // Crear 
                    string upload = webRootPath + WC.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    using(var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    productoVM.Producto.ImagenUrl = fileName + extension;
                    _db.Productos.Add(productoVM.Producto);
                }
                else
                {
                    // Actualizar
                    var objProducto = _db.Productos.AsNoTracking().FirstOrDefault(p => p.Id == productoVM.Producto.Id);
                    if(files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagenRuta;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productoVM.Producto.ImagenUrl = fileName + extension;
                    }
                    else
                    {
                        productoVM.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _db.Productos.Update(productoVM.Producto);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            // se llenan nuevamente las listas si algo falla
            productoVM.CategoriaLista = _db.Categorias.Select(c => new SelectListItem
            {
                Text = c.NombreCategoria,
                Value = c.Id.ToString()
            });
            productoVM.TipoAplicacionList = _db.TipoAplicaciones.Select(t => new SelectListItem
            {
                Text = t.NombreTipAplicacion,
                Value = t.Id.ToString()
            });

            return View(productoVM);
        }

        // GET Eliminar
        public IActionResult Eliminar(int? id)
        {            
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Producto producto = _db.Productos.Include(c => c.Categoria)
                                             .Include(t => t.TipoAplicacion)
                                             .FirstOrDefault(p => p.Id == id);
            
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Eliminar(Producto producto)
        {
            if (producto == null)
            {
                return NotFound();
            }
            // Cargar la imagen a eliminar
            string upload = _webHostEnvironment.WebRootPath + WC.ImagenRuta;

            // Borrar imagen anterior
            var anteriorFile = Path.Combine(upload, producto.ImagenUrl);
            if (System.IO.File.Exists(anteriorFile))
            {
                System.IO.File.Delete(anteriorFile);
            }

            _db.Productos.Remove(producto);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
