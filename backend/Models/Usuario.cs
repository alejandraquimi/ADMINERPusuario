
namespace backend.Models;

public class Usuario
    {
        public int IDUsuario { get; set; }

        public string? Nombre { get; set; }

        public string CorreoElectronico { get; set; } = null!;

        public string ContrasenaHash { get; set; } = null!;

        public string? Telefono { get; set; }
        public string? EstadoRegistro {get;set;}
        public DateTime FechaCreacion { get; set; }

    public virtual ICollection<Contacto> UsuariosContactos { get; set; } = new List<Contacto>();
    public virtual ICollection<Contacto>? UsuariosPrincipal { get; set; }

    }