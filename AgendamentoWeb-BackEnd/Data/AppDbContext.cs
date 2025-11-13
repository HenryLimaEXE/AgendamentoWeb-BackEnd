using Microsoft.EntityFrameworkCore;
using SchedulingSystem.API.Models;

namespace SchedulingSystem.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração do User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(u => u.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Configuração da Tarefa
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasOne(t => t.User)
                      .WithMany()
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(t => t.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(t => t.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(t => t.UserId);
                entity.HasIndex(t => t.Status);
            });
        }
    }
}