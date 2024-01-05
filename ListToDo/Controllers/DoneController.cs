using ListToDo.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ListToDo.Controllers
{
    public class DoneController: Controller
    {
        private readonly IRepositorioToDoDone repositorioToDoDone;


        public DoneController(IRepositorioToDoDone repositorioToDoDone
             )
        {
            this.repositorioToDoDone = repositorioToDoDone;

        }
        public async Task<IActionResult> Index2()
        {
            var id_usuario = 1;
            var toDoDone = await repositorioToDoDone.ObtenerToDoDone(id_usuario);

            Console.WriteLine(toDoDone.Count);

            return View(toDoDone);
        }
    }
}
