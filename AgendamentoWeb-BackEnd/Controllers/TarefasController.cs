using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Services;

namespace SchedulingSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefasController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpGet("usuario/{userId}")]
        public async Task<IActionResult> GetTarefasPorUsuario(int userId)
        {
            try
            {
                var tarefas = await _tarefaService.ObterTarefasPorUsuarioAsync(userId);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CriarTarefa([FromBody] CriarTarefaDto tarefaDto)
        {
            try
            {
                var tarefa = await _tarefaService.CriarTarefaAsync(tarefaDto);
                return Ok(new { message = "Tarefa criada com sucesso", tarefa });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] EditarTarefaDto tarefaDto)
        {
            try
            {
                var tarefa = await _tarefaService.AtualizarTarefaAsync(id, tarefaDto);
                if (tarefa == null)
                    return NotFound(new { message = "Tarefa não encontrada" });

                return Ok(new { message = "Tarefa atualizada com sucesso", tarefa });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirTarefa(int id)
        {
            try
            {
                var result = await _tarefaService.ExcluirTarefaAsync(id);
                if (!result)
                    return NotFound(new { message = "Tarefa não encontrada" });

                return Ok(new { message = "Tarefa excluída com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/alternar-status")]
        public async Task<IActionResult> AlternarStatusTarefa(int id)
        {
            try
            {
                var result = await _tarefaService.AlternarStatusTarefaAsync(id);
                if (!result)
                    return NotFound(new { message = "Tarefa não encontrada" });

                return Ok(new { message = "Status da tarefa alterado com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}/mover-fazendo")]
        public async Task<IActionResult> MoverParaFazendo(int id)
        {
            try
            {
                var result = await _tarefaService.MoverParaFazendoAsync(id);
                if (!result)
                    return NotFound(new { message = "Tarefa não encontrada" });

                return Ok(new { message = "Tarefa movida para 'Fazendo' com sucesso" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}