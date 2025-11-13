using System.ComponentModel.DataAnnotations;

namespace SchedulingSystem.API.DTOs
{
    public class TarefaDto
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string DataLimite { get; set; }

        [Required]
        public string Descricao { get; set; }

        public bool Concluida { get; set; }

        [Required]
        public string Status { get; set; }

        public int UserId { get; set; }
    }

    public class CriarTarefaDto
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string DataLimite { get; set; }

        [Required]
        public string Descricao { get; set; }

        public int UserId { get; set; }
    }

    public class EditarTarefaDto
    {
        [Required]
        public string Titulo { get; set; }

        [Required]
        public string DataLimite { get; set; }

        [Required]
        public string Descricao { get; set; }

        public bool Concluida { get; set; }

        [Required]
        public string Status { get; set; }
    }
}