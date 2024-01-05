using ListToDo.Models;
using ListToDo.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ListToDo.Controllers
{
    public class TodoController: Controller
    {
        private readonly IRepositorioToDo repositorioToDo;

        public TodoController(IRepositorioToDo repositorioToDo)
        {
            this.repositorioToDo = repositorioToDo;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Crear(ToDoModel toDo)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            toDo.id_usuario = 1;
            await repositorioToDo.crearTodo(toDo);


            return RedirectToAction("Principal");
        }

        public async Task<IActionResult> Principal(ToDoModel toDo)
        {
            var id_usuario = 1;
            var modelo = await repositorioToDo.ObtenerToDo(id_usuario);
            Console.WriteLine(modelo.Count + "Hola");
            return View(modelo);
        }

        public async Task<IActionResult> Editar(int id)
        {   
            var usuarioId = 1;
            var cuenta = await repositorioToDo.ObtenerById(id, usuarioId);

            if (cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }

            return View(cuenta);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(ToDoModel toDo)
        {
            var usuario = 1;
            var cuenta = await repositorioToDo.ObtenerById(toDo.Id, usuario);

            if (cuenta is null)
            {
                return RedirectToAction("NoEncontrado", "Home");
            }
 
            await repositorioToDo.Actualizar(toDo);


            return RedirectToAction("Principal");

        }

        public async Task<IActionResult>Done(int id)
        {
            var usuarioId = 1;
         
            var todo = await repositorioToDo.ObtenerById(id, usuarioId);
            if (todo is null)
            {
                
                return RedirectToAction("NoEncontrado", "Home");
            }
            Console.WriteLine(id + "hola todos 11");




            return View(todo);
        }

        [HttpPost]
        public async Task<IActionResult> DonePost(int id)
        {
            Console.WriteLine("Ehola");

            var usuarioId = 1;
            var todo = await repositorioToDo.ObtenerById(id, usuarioId);
            
            if (todo is null)
            {
                
                return RedirectToAction("NoEncontrado", "Home");
            }

            await repositorioToDo.ActualizarDone(todo);

            return RedirectToAction("Index", "Home");
        }

    }
}
