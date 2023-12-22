using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class MContacto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioContacto",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioPrincipal",
                table: "Contactos");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioContacto",
                table: "Contactos",
                column: "IDUsuarioContacto",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioPrincipal",
                table: "Contactos",
                column: "IDUsuarioPrincipal",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioContacto",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioPrincipal",
                table: "Contactos");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioContacto",
                table: "Contactos",
                column: "IDUsuarioContacto",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioPrincipal",
                table: "Contactos",
                column: "IDUsuarioPrincipal",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario");
        }
    }
}
