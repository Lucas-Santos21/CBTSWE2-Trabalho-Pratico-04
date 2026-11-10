//Nome e prontuário da dupla:
//Lucas da Silva Santos CB3030598
//Kaueh Farias Ferreira dos Santos CB3031438

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TP04.Domain.Models;
using TP04.Web.Data;

namespace TP04.Web.Controllers.Api
{
    [ApiController]
    [Route("api/tarefas")]
    public class TarefasApiController : ControllerBase
    {
        private readonly AppDbContext _contexto;

        public TarefasApiController(AppDbContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodas()
        {
            var tarefas = await _contexto.Tarefas
                .OrderBy(tarefa => tarefa.DataVencimento)
                .ToListAsync();

            return Ok(tarefas);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            Tarefa? tarefa = await _contexto.Tarefas
                .FirstOrDefaultAsync(tarefa => tarefa.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return Ok(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Tarefa tarefa)
        {
            tarefa.Id = 0;
            tarefa.DataCriacao = DateTime.Now;

            _contexto.Tarefas.Add(tarefa);

            await _contexto.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPorId),
                new { id = tarefa.Id },
                tarefa);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, Tarefa tarefa)
        {
            if (tarefa.Id != 0 && tarefa.Id != id)
            {
                return BadRequest("O ID da URL não corresponde ao ID da tarefa.");
            }

            Tarefa? tarefaExistente = await _contexto.Tarefas
                .FirstOrDefaultAsync(tarefa => tarefa.Id == id);

            if (tarefaExistente == null)
            {
                return NotFound();
            }

            tarefaExistente.Titulo = tarefa.Titulo;
            tarefaExistente.Descricao = tarefa.Descricao;
            tarefaExistente.DataVencimento = tarefa.DataVencimento;
            tarefaExistente.Status = tarefa.Status;
            tarefaExistente.Prioridade = tarefa.Prioridade;

            await _contexto.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Tarefa? tarefa = await _contexto.Tarefas
                .FirstOrDefaultAsync(tarefa => tarefa.Id == id);

            if (tarefa == null)
            {
                return NotFound();
            }

            _contexto.Tarefas.Remove(tarefa);

            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}