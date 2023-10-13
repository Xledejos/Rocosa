using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rocosa_AccesoDatos.Datos.Repository.IRepository;
using Rocosa_Modelos;
using Rocosa_Utilidades;

namespace Rocosa.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _catRepo;

        public CategoriaController(ICategoriaRepository catRepo)
        {
            _catRepo = catRepo;
        }

        public async Task<IActionResult> Index()
        {
           _catRepo.GetAll();

            return View();
        }

        // GET CREAR
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(Categoria categoria)
        {
            if(ModelState.IsValid)
            {
                _catRepo.Insert(categoria);
                _catRepo.Save();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET EDITAR
        public async Task<IActionResult> Editar(int? id)
        {
            if(id == null || id == 0)
            {
                NotFound();
            }
            var obj = await _catRepo.GetById(id.GetValueOrDefault());

            if(obj == null)
            {
                NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _catRepo.Update(categoria);
                _catRepo.Save();

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET ELIMINAR
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = await _catRepo.GetById(id.GetValueOrDefault());

            if (obj == null)
            {
                return NotFound();
            }


            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(Categoria categoria)
        {
            if (categoria == null)
            {
                return NotFound();
            }
            
            _catRepo.Delete(categoria);
            _catRepo.Save();

            return RedirectToAction("Index");
        }
    }
}
