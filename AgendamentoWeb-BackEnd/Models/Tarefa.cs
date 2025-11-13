using System.ComponentModel.DataAnnotations;

namespace SchedulingSystem.API.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string DataLimite { get; set; } // Mantém como string para compatibilidade

        [Required]
        public string Descricao { get; set; }

        public bool Concluida { get; set; } = false;

        [Required]
        public string Status { get; set; } = "pendente"; // pendente, fazendo, concluido

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}