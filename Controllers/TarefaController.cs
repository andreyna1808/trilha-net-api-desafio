using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Services;
using TrilhaApiDesafio.Validations;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;
        private readonly TarefaService tarefaService;

        public TarefaController(OrganizadorContext context, TarefaService tarefaService)
        {
            _context = context;
            this.tarefaService = tarefaService;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefaById = tarefaService.FindById(id);
            if (tarefaById == null)
            {
                return NotFound();
            }

            return Ok(tarefaById);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var allTarefas = tarefaService.FindAll();
            return Ok(allTarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            var tarefaByTitle = tarefaService.FindBy(EnumFilterType.Title, titulo);
            if (tarefaByTitle == null)
            {
                return NotFound();
            }

            return Ok(tarefaByTitle);
        }

        [HttpGet("ObterPorDate")]
        public IActionResult ObterPorDate(DateTime date)
        {
            var tarefaByDate = tarefaService.FindBy(EnumFilterType.Date, date);
            if (tarefaByDate == null)
            {
                return NotFound();
            }

            return Ok(tarefaByDate);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefaByStatus = tarefaService.FindBy(EnumFilterType.Status, status);
            if (tarefaByStatus == null)
            {
                return NotFound();
            }

            return Ok(tarefaByStatus);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            var errorMsg = ValidationTarefas.ValidateDate(tarefa);
            if (errorMsg != null)
                return BadRequest(new { Erro = errorMsg });

            tarefaService.Add(tarefa);
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = tarefaService.FindById(id);
            if (tarefaBanco == null)
                return NotFound();

            var errorMsg = ValidationTarefas.ValidateDate(tarefa);
            if (errorMsg != null)
                return BadRequest(new { Erro = errorMsg });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Date = tarefa.Date;
            tarefaBanco.Status = tarefa.Status;
            tarefaBanco.Descricao = tarefa.Descricao;

            tarefaService.Update(tarefaBanco);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = tarefaService.FindById(id);

            if (tarefaBanco == null)
                return NotFound();

            tarefaService.Delete(tarefaBanco);
            return NoContent();
        }
    }
}