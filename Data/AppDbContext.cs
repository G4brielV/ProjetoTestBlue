using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options){}
        public DbSet<Usuario> ProjTestBlue {get; set;}
    }
}