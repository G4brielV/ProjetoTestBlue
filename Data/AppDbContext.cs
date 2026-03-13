using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<Usuario> ProjTestBlue {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da Entidade Usuario via Fluent API
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios"); // Nome da tabela
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Nome)
                    .HasColumnName("nome")
                    .HasMaxLength(50)
                    .IsRequired(); 
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired();
                entity.Property(e => e.Senha)
                    .HasColumnName("senha")
                    .IsRequired();
                
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }
    }
}