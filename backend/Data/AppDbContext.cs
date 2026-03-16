using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<Usuario> Usuarios {get; set;}
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<Card> Cards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuarios"); // Nome da tabela
                entity.HasKey(e => e.Id); // Chave primária
                entity.Property(e => e.Id)
                    .HasColumnName("id");
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

            modelBuilder.Entity<TodoList>(entity => {
                entity.ToTable("todo_lists"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(e => e.Titulo)
                    .HasColumnName("titulo")
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(c => c.UsuarioId)
                    .HasColumnName("usuario_id");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.TodoLists)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade); 
                
            });


            modelBuilder.Entity<Card>(entity => {
                entity.ToTable("cards"); 
                entity.HasKey(e => e.Id); 
                entity.Property(e => e.Id)
                    .HasColumnName("id");
                entity.Property(c => c.Titulo)
                    .HasColumnName("title")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(c => c.Descricao)
                    .HasColumnName("descricao")
                    .HasColumnType("text");
                entity.Property(c => c.IsCompleted)
                    .HasColumnName("is_completed")
                    .HasDefaultValue(false);
                entity.Property(c => c.Posicao)
                    .HasColumnName("posicao")
                    .IsRequired();
                entity.Property(c => c.ListId)
                    .HasColumnName("list_id");
                
                entity.HasOne(d => d.TodoList)
                    .WithMany(p => p.Cards)
                    .HasForeignKey(d => d.ListId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Usuario padrao:
            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id = 1,
                Nome = "Teste",
                Email = "teste@example.com",
                Senha = "$2a$11$7hQ9zfoWMuNPvvlcwizuEOSvE0AKZNDS0cHWrK16Abr9PwsvtdCFG" 
            });
        }         
    }
}