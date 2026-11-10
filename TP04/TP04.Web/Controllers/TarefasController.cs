//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP04.Web.Data;

namespace TP04.Web.Controllers
{
    public class TarefasController : Controller
    {
        private readonly AppDbContext _contexto;

        public TarefasController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var tarefa = await _contexto.Tarefas
                .FirstOrDefaultAsync(tarefa => tarefa.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }
    }
}