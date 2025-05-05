using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace ProjetoAPI.Controllers
{   
    [Route("usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDataContext _context;
        private readonly IConfiguration _config;

        public UsuariosController(AppDataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("cadastrar")]
        public async Task<IActionResult> Cadastrar([FromBody] Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                return BadRequest("Email já cadastrado");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == login.Email && u.Senha == login.Senha);
            if (usuario == null) return Unauthorized();

            var token = GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        [HttpGet("listar")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public async Task<IActionResult> Listar()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["JwtKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}