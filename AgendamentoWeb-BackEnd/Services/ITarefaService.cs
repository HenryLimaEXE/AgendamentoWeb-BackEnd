using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Models;

namespace SchedulingSystem.API.Services
{
    public interface ITarefaService
    {
        Task<Tarefa> CriarTarefaAsync(CriarTarefaDto tarefaDto);
        Task<List<Tarefa>> ObterTarefasPorUsuarioAsync(int userId);
        Task<Tarefa?> ObterTarefaPorIdAsync(int id); 
        Task<Tarefa?> AtualizarTarefaAsync(int id, EditarTarefaDto tarefaDto);
        Task<bool> ExcluirTarefaAsync(int id);
        Task<bool> AlternarStatusTarefaAsync(int id);
        Task<bool> MoverParaFazendoAsync(int id);
    }
}