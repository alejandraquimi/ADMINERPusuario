using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Services;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
       private readonly BackendDbContext _context;

        public ContactoController(BackendDbContext context)
        {
            _context = context;
        }
        [HttpGet("{idUsuario}")]
        public async Task<ActionResult<List<Contacto>>> GetContactosPorUsuarioP(int idUsuario)
        {
            try
            {
                 var contactos = await _context.Contactos
            .Include(c => c.UsuarioC)  
            .Where(c => c.IDUsuarioP == idUsuario)
            .Select(contacto => new Contacto
            {
                IDContacto=contacto.IDContacto,
                UsuarioC = new Usuario
                {
                    IDUsuario = contacto.UsuarioP.IDUsuario,
                    Nombre = contacto.UsuarioP.Nombre,
                    CorreoElectronico = contacto.UsuarioP.CorreoElectronico,
                    FechaCreacion=contacto.UsuarioP.FechaCreacion,
                    Telefono=contacto.UsuarioP.Telefono,
                    
                },
                IDUsuarioP = contacto.IDUsuarioP
            })
            .ToListAsync();

                if (contactos == null || contactos.Count == 0)
                {
                    return NotFound("No se encontraron contactos para el usuario especificado");
                }

                return Ok(contactos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


       [HttpPost]
        public async Task<ActionResult<Contacto>> AddContacto(Contacto contacto)
        {
            try
            {
                if (_context.Contactos.Any(c => c.IDUsuarioC == contacto.IDUsuarioC && c.IDUsuarioP == contacto.IDUsuarioP))
                {
                    return BadRequest("Ya existe este usuario entre sus contactos.");
                }
                else{
                    
                    _context.Contactos.Add(contacto);
                    await _context.SaveChangesAsync();
                    return Ok(contacto);
                        
                }

                 }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        
    }
}
