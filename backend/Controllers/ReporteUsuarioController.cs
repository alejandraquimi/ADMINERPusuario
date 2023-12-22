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
    public class ReporteUsuarioController : ControllerBase
    {
       private readonly BackendDbContext _context;

        public ReporteUsuarioController(BackendDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> GetDatosReporteUsuarioFiltros([FromBody] ReporteFilterUsuario usuarioFilter)
        {
            var usuarios =   _context.Usuarios.Where(u => u.EstadoRegistro == "A")
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
                }).AsQueryable();
            if(usuarios==null){
                return NotFound("No existe usuarios ");

            }
            if (usuarioFilter.idUsuario.HasValue && usuarioFilter.query==null )
            {
                usuarios = usuarios.Where(u => u.IDUsuario == usuarioFilter.idUsuario.Value);

            }
            else if (usuarioFilter.idUsuario.HasValue && usuarioFilter.query!=null)
            {
                usuarios = usuarios
                        .Where(u => u.IDUsuario != usuarioFilter.idUsuario.Value)
                        .Where(u => u.Nombre.Contains(usuarioFilter.query) || u.CorreoElectronico.Contains(usuarioFilter.query));

            }
           
            if (usuarios == null|| !usuarios.Any())
            {
                return NotFound("Usuario no encontrado");
            }
            
            return Ok(usuarios);
                    
        }
        
       
    }
}
