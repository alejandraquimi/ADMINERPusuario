using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Creacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IDUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContrasenaHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    IDContacto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDUsuarioPrincipal = table.Column<int>(type: "int", nullable: true),
                    IDUsuarioContacto = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.IDContacto);
                    table.ForeignKey(
                        name: "FK_Contactos_Usuarios_IDUsuarioContacto",
                        column: x => x.IDUsuarioContacto,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario");
                    table.ForeignKey(
                        name: "FK_Contactos_Usuarios_IDUsuarioPrincipal",
                        column: x => x.IDUsuarioPrincipal,
                        principalTable: "Usuarios",
                        principalColumn: "IDUsuario");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_IDUsuarioContacto",
                table: "Contactos",
                column: "IDUsuarioContacto");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_IDUsuarioPrincipal",
                table: "Contactos",
                column: "IDUsuarioPrincipal");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
