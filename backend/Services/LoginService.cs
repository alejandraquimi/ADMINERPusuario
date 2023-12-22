using backend.Models;
namespace backend.Services;
public class LoginService
{
    private readonly BackendDbContext _context;
            private readonly IPasswordHasher _passwordHasher;


    public LoginService(BackendDbContext context, IPasswordHasher passwordHasher)
    {
        this._context=context;
        this._passwordHasher=passwordHasher;

    }
    public async Task<Usuario?> GetUser(Usuario usuario)
        {
            var userInDb = await _context.Usuarios
                .SingleOrDefaultAsync(x => x.CorreoElectronico == usuario.CorreoElectronico && x.EstadoRegistro == "A");

            if (userInDb == null || !_passwordHasher.VerifyPassword(usuario.ContrasenaHash, userInDb.ContrasenaHash))
            {
                return null;
            }

            return userInDb;
        }
}