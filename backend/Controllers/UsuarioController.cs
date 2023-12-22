using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Entities;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
       private readonly BackendDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsuarioController(BackendDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> GetUsuarios()
        {
            var usuariosConContactos = await _context.Usuarios
                .Select(usuario => new Usuario
                {
                    IDUsuario = usuario.IDUsuario,
                    Nombre = usuario.Nombre,
                    CorreoElectronico = usuario.CorreoElectronico,
                    FechaCreacion=usuario.FechaCreacion,
                    Telefono=usuario.Telefono,
                    UsuariosContactos = usuario.UsuariosContactos
                        .Select(contacto => new Contacto
                        {
                            IDContacto = contacto.IDContacto,
                            IDUsuarioC = contacto.IDUsuarioC,

                            IDUsuarioP = usuario.IDUsuario,
                        })
                        .ToList()
                })
                .ToListAsync();
            if (usuariosConContactos == null)
            {
                return NotFound("No se encontro información");
            }

            return Ok(usuariosConContactos);
        }

       

        
        [HttpPost]
        public async Task<ActionResult<Usuario>> AddUsuario(Usuario usuario)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == usuario.CorreoElectronico);

            if (usuarioExistente != null)
            {
                return BadRequest("El correo electrónico ya está registrado. No se puede agregar el usuario.");
            }
            else
            {
         
                usuario.ContrasenaHash = _passwordHasher.HashPassword(usuario.ContrasenaHash);

                usuario.FechaCreacion = DateTime.UtcNow;
                usuario.EstadoRegistro="A";

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return Ok(await _context.Usuarios.ToListAsync());
            }
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes personalizar según tus necesidades)
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<ActionResult<UsuarioEntity>> UpdateUsuario(UsuarioEntity request)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.IDUsuario == request.idUsuario && u.EstadoRegistro == "A");
            if (usuario == null)
            { 
                return NotFound("Usuario no encontrada"); 
            }
            // Herramienta
            if (request.nombre != null)
            {
                usuario.Nombre = request.nombre;
            }

            if (request.correoElectronico != null)
            {
                usuario.CorreoElectronico = request.correoElectronico;
            }
            if (request.contrasenaHash != null)
            {
                usuario.ContrasenaHash =_passwordHasher.HashPassword(request.contrasenaHash);
            }
            if (request.telefono != null)
            {
                usuario.Telefono = request.telefono;
            }


            _context.SaveChanges();
            return  Ok(usuario);
        }

       [HttpDelete("{idUsuario}")]
        public async Task<ActionResult> InactivarUsuario(int idUsuario)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(idUsuario);

                if (usuario == null)
                {
                    return NotFound("Usuario no encontrado");
                }

                usuario.EstadoRegistro = "I";

                await _context.SaveChangesAsync();

                return Ok($"Usuario con ID {idUsuario} inactivado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}
