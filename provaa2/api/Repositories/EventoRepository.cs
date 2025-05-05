using Microsoft.EntityFrameworkCore;
using ProjetoAPI.Data;
using ProjetoAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoAPI.Repositories
{
    public class EventoRepository
    {
        private readonly AppDataContext _context;

        public EventoRepository(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Evento>> GetAllAsync()
        {
            return await _context.Eventos.ToListAsync();
        }

        public async Task<List<Evento>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _context.Eventos
                .Where(e => e.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Evento> CreateAsync(Evento evento)
        {
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return evento;
        }
    }
}