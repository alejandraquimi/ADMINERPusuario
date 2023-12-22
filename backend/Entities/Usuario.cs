namespace backend.Entities
{
    public class ReporteFilterUsuario
    {
        public int? idUsuario { get; set; }
        public string? query { get; set; }
    }
    public class UsuarioEntity
    {
       public int? idUsuario { get; set; }

        public string? nombre { get; set; }

        public string? correoElectronico { get; set; } = null!;

        public string? contrasenaHash { get; set; } = null!;

        public string? telefono { get; set; }
        public DateTime? fechaCreacion { get; set; }
    }
}
