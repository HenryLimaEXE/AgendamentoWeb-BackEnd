using Microsoft.EntityFrameworkCore;
using SchedulingSystem.API.Data;
using SchedulingSystem.API.DTOs;
using SchedulingSystem.API.Models;

namespace SchedulingSystem.API.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly AppDbContext _context;

        public TarefaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> CriarTarefaAsync(CriarTarefaDto tarefaDto)
        {
            var tarefa = new Tarefa
            {
                Titulo = tarefaDto.Titulo,
                DataLimite = tarefaDto.DataLimite,
                Descricao = tarefaDto.Descricao,
                UserId = tarefaDto.UserId,
                Status = "pendente",
                Concluida = false
            };

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<List<Tarefa>> ObterTarefasPorUsuarioAsync(int userId)
        {
            return await _context.Tarefas
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<Tarefa?> ObterTarefaPorIdAsync(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa?> AtualizarTarefaAsync(int id, EditarTarefaDto tarefaDto)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return null;

            tarefa.Titulo = tarefaDto.Titulo;
            tarefa.DataLimite = tarefaDto.DataLimite;
            tarefa.Descricao = tarefaDto.Descricao;
            tarefa.Concluida = tarefaDto.Concluida;
            tarefa.Status = tarefaDto.Status;

            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> ExcluirTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AlternarStatusTarefaAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;

            tarefa.Concluida = !tarefa.Concluida;
            tarefa.Status = tarefa.Concluida ? "concluido" : "pendente";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MoverParaFazendoAsync(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null) return false;

            tarefa.Status = "fazendo";
            tarefa.Concluida = false;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}