using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjetoAPI.Data;
using ProjetoAPI.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração básica dos serviços
builder.Services.AddControllers();

// Configuração do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDataContext>(options => 
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Registro dos repositórios (sem interfaces)
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<EventoRepository>();

// Configuração do JWT
var jwtKey = builder.Configuration["JwtKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Pipeline de requisições HTTP
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();