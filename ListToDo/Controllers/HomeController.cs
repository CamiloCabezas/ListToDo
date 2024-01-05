using ListToDo.Models;
using ListToDo.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ListToDo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositorioToDo repositorioToDo;

        public HomeController(ILogger<HomeController> logger, IRepositorioToDo repositorioToDo)
        {
            _logger = logger;
            this.repositorioToDo = repositorioToDo;
        }

        public async Task<IActionResult> Index()

        {
            var id_usuario = 1;

            var toDoModels = new ToDViewModel
            {
                toDoDones = await repositorioToDo.ObtenerToDoDone(id_usuario),
                ToDoList = await repositorioToDo.ObtenerToDo(id_usuario)
            };

            return View(toDoModels);
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