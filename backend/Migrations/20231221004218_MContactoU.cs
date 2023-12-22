using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class MContactoU : Migration
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

            migrationBuilder.RenameColumn(
                name: "IDUsuarioPrincipal",
                table: "Contactos",
                newName: "IDUsuarioP");

            migrationBuilder.RenameColumn(
                name: "IDUsuarioContacto",
                table: "Contactos",
                newName: "IDUsuarioC");

            migrationBuilder.RenameIndex(
                name: "IX_Contactos_IDUsuarioPrincipal",
                table: "Contactos",
                newName: "IX_Contactos_IDUsuarioP");

            migrationBuilder.RenameIndex(
                name: "IX_Contactos_IDUsuarioContacto",
                table: "Contactos",
                newName: "IX_Contactos_IDUsuarioC");

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioC",
                table: "Contactos",
                column: "IDUsuarioC",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioP",
                table: "Contactos",
                column: "IDUsuarioP",
                principalTable: "Usuarios",
                principalColumn: "IDUsuario",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioC",
                table: "Contactos");

            migrationBuilder.DropForeignKey(
                name: "FK_Contactos_Usuarios_IDUsuarioP",
                table: "Contactos");

            migrationBuilder.RenameColumn(
                name: "IDUsuarioP",
                table: "Contactos",
                newName: "IDUsuarioPrincipal");

            migrationBuilder.RenameColumn(
                name: "IDUsuarioC",
                table: "Contactos",
                newName: "IDUsuarioContacto");

            migrationBuilder.RenameIndex(
                name: "IX_Contactos_IDUsuarioP",
                table: "Contactos",
                newName: "IX_Contactos_IDUsuarioPrincipal");

            migrationBuilder.RenameIndex(
                name: "IX_Contactos_IDUsuarioC",
                table: "Contactos",
                newName: "IX_Contactos_IDUsuarioContacto");

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
    }
}
