using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class BackendDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=ALEJANDRA\\MSSQLSERVER01;Database=test2;Trusted_Connection=True;TrustServerCertificate=True;");



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().HasKey(c => c.IDUsuario);

            modelBuilder.Entity<Contacto>().HasKey(c => c.IDContacto);

            modelBuilder.Entity<Usuario>().Property(e => e.FechaCreacion)
            .HasDefaultValueSql("GETDATE()");

            
            modelBuilder.Entity<Contacto>()
            .HasOne(c => c.UsuarioC)
            .WithMany(u=>u.UsuariosContactos)
            .HasForeignKey(c => c.IDUsuarioP)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Contacto>()
            .HasOne(c => c.UsuarioP)
            .WithMany(u => u.UsuariosPrincipal)
            .HasForeignKey(c => c.IDUsuarioC)
            .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }
}
