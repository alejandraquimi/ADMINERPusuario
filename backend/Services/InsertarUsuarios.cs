using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Services
{
    public class ScriptExecutor
    {
     private readonly IServiceScopeFactory _serviceScopeFactory;

    public ScriptExecutor(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task ExecuteScript()
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BackendDbContext>();

                   try
        {
            var nuevoRegistro = new Usuario
            {
                Nombre = "admin",
                CorreoElectronico = "admin@hotmail.com",
                ContrasenaHash = "admin",
                Telefono = "2000000"
            };

            var usuarioExistente = await dbContext.Usuarios
                .FirstOrDefaultAsync(u => u.CorreoElectronico == nuevoRegistro.CorreoElectronico);

            if (usuarioExistente != null)
            {
                Console.WriteLine("El correo electrónico ya está registrado. No se puede agregar el usuario.");
            }
            else
            {
                var _passwordHasher = new BCryptPasswordHasher();
                nuevoRegistro.ContrasenaHash = _passwordHasher.HashPassword(nuevoRegistro.ContrasenaHash);
                nuevoRegistro.FechaCreacion = DateTime.UtcNow;
                nuevoRegistro.EstadoRegistro = "A";

                dbContext.Usuarios.Add(nuevoRegistro);
                await dbContext.SaveChangesAsync();

                Console.WriteLine("Usuario registrado exitosamente.");
            }
            }
        catch (Exception ex)
        {
                Console.WriteLine($"Error al agregar el registro: {ex.Message}");
        }     

        }
    }
}
