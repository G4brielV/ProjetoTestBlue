using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Repository.Impl
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> FindByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }

        public async Task<Usuario?> FindByEmailAsync(String email)
        {
            var usuario = await _context.Usuarios.SingleOrDefaultAsync(u => u.Email == email);
            return usuario;
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario usuarioNovo)
        {
            await _context.SaveChangesAsync();
            return usuarioNovo;
        }

        public async Task<bool> DeleteUsuarioAsync(Usuario usuario)
        {
            _context.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return usuarios;
        }

    }
}