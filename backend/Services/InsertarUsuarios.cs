using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
namespace backend.Services;
public class ScriptExecutor
{
    public async Task ExecuteScript()
    {
        using var dbContext = new BackendDbContext();
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
