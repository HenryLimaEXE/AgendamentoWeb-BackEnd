using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchedulingSystem.API.Models
{
    [Table("Tarefas")]
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string DataLimite { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "text")]
        public string Descricao { get; set; } = string.Empty;

        public bool Concluida { get; set; } = false;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "pendente";

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}