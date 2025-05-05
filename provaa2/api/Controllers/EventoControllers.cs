using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using System.Security.Claims;

namespace ProjetoAPI.Controllers
{
    [Route("eventos")]
    [ApiController]
    public class EventosController : ControllerBase
    {
        private readonly AppDataContext _context;

        public EventosController(AppDataContext context)
        {
            _context = context;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _context.Eventos.ToListAsync());
        }

        [HttpGet("usuario")]
        [Authorize]
        public async Task<IActionResult> ListarPorUsuario()
        {
            var usuarioId = int.Parse(User.FindFirst("id").Value);
            return Ok(await _context.Eventos.Where(e => e.UsuarioId == usuarioId).ToListAsync());
        }

        [HttpPost("cadastrar")]
        [Authorize]
        public async Task<IActionResult> Cadastrar([FromBody] Evento evento)
        {
            evento.UsuarioId = int.Parse(User.FindFirst("id").Value);
            _context.Eventos.Add(evento);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}