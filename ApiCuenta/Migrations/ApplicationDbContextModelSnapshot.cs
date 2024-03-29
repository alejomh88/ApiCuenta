﻿// <auto-generated />
using System;
using ApiCuenta.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ApiCuenta.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApiCuenta.Modelos.Cuenta", b =>
                {
                    b.Property<string>("Numero")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Identificacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Saldo")
                        .HasColumnType("real");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Numero");

                    b.ToTable("Cuenta");
                });

            modelBuilder.Entity("ApiCuenta.Modelos.Movimiento", b =>
                {
                    b.Property<int>("IdMovimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMovimiento"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<float>("Saldo")
                        .HasColumnType("real");

                    b.Property<string>("TipoMovimiento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Valor")
                        .HasColumnType("real");

                    b.HasKey("IdMovimiento");

                    b.HasIndex("Numero");

                    b.ToTable("Movimimento");
                });

            modelBuilder.Entity("ApiCuenta.Modelos.Movimiento", b =>
                {
                    b.HasOne("ApiCuenta.Modelos.Cuenta", "Cuenta")
                        .WithMany()
                        .HasForeignKey("Numero")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cuenta");
                });
#pragma warning restore 612, 618
        }
    }
}
