using System.ComponentModel.DataAnnotations;

namespace SchedulingSystem.API.DTOs
{
    public class TarefaDto
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string DataLimite { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        public bool Concluida { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        public int UserId { get; set; }
    }

    public class CriarTarefaDto
    {
        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string DataLimite { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        public int UserId { get; set; }
    }

    public class EditarTarefaDto
    {
        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public string DataLimite { get; set; } = string.Empty;

        [Required]
        public string Descricao { get; set; } = string.Empty;

        public bool Concluida { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;
    }
}