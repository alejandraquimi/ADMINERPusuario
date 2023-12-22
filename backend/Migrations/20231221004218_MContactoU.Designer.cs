﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using backend.Models;

#nullable disable

namespace backend.Migrations
{
    [DbContext(typeof(BackendDbContext))]
    [Migration("20231221004218_MContactoU")]
    partial class MContactoU
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("backend.Models.Contacto", b =>
                {
                    b.Property<int>("IDContacto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDContacto"));

                    b.Property<int?>("IDUsuarioC")
                        .HasColumnType("int");

                    b.Property<int?>("IDUsuarioP")
                        .HasColumnType("int");

                    b.HasKey("IDContacto");

                    b.HasIndex("IDUsuarioC");

                    b.HasIndex("IDUsuarioP");

                    b.ToTable("Contactos");
                });

            modelBuilder.Entity("backend.Models.Usuario", b =>
                {
                    b.Property<int>("IDUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDUsuario"));

                    b.Property<string>("ContrasenaHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaCreacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IDUsuario");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("backend.Models.Contacto", b =>
                {
                    b.HasOne("backend.Models.Usuario", "UsuarioC")
                        .WithMany("UsuariosContactos")
                        .HasForeignKey("IDUsuarioC")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("backend.Models.Usuario", "UsuarioP")
                        .WithMany("UsuariosPrincipal")
                        .HasForeignKey("IDUsuarioP")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("UsuarioC");

                    b.Navigation("UsuarioP");
                });

            modelBuilder.Entity("backend.Models.Usuario", b =>
                {
                    b.Navigation("UsuariosContactos");

                    b.Navigation("UsuariosPrincipal");
                });
#pragma warning restore 612, 618
        }
    }
}
