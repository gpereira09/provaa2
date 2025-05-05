using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Repositories
{
    public class UsuarioRepository
    {
        private readonly AppDataContext _context;

        public UsuarioRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<Usuario> CreateAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> GetByEmailAndPasswordAsync(string email, string senha)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }
    }
}