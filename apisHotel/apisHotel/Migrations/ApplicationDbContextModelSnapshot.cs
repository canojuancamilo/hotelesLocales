﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using apisHotel;

#nullable disable

namespace apisHotel.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("apisHotel.Models.ContactoEmergencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelefonoContacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContactosEmergencia");
                });

            modelBuilder.Entity("apisHotel.Models.Habitacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostoBase")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Habilitada")
                        .HasColumnType("bit");

                    b.Property<int?>("HotelId")
                        .HasColumnType("int");

                    b.Property<decimal>("Impuestos")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("Habitaciones");
                });

            modelBuilder.Entity("apisHotel.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Habilitado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hoteles");
                });

            modelBuilder.Entity("apisHotel.Models.Huesped", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ReservaId")
                        .HasColumnType("int");

                    b.Property<string>("TelefonoContacto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReservaId");

                    b.ToTable("Huespedes");
                });

            modelBuilder.Entity("apisHotel.Models.Reserva", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CantidadPersonas")
                        .HasColumnType("int");

                    b.Property<int>("ContactoEmergenciaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaEntrada")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaSalida")
                        .HasColumnType("datetime2");

                    b.Property<int>("HabitacionId")
                        .HasColumnType("int");

                    b.Property<int>("HotelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContactoEmergenciaId");

                    b.HasIndex("HabitacionId");

                    b.HasIndex("HotelId");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("apisHotel.Models.Habitacion", b =>
                {
                    b.HasOne("apisHotel.Models.Hotel", null)
                        .WithMany("Habitaciones")
                        .HasForeignKey("HotelId");
                });

            modelBuilder.Entity("apisHotel.Models.Huesped", b =>
                {
                    b.HasOne("apisHotel.Models.Reserva", null)
                        .WithMany("Huespedes")
                        .HasForeignKey("ReservaId");
                });

            modelBuilder.Entity("apisHotel.Models.Reserva", b =>
                {
                    b.HasOne("apisHotel.Models.ContactoEmergencia", "ContactoEmergencia")
                        .WithMany()
                        .HasForeignKey("ContactoEmergenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apisHotel.Models.Habitacion", "Habitacion")
                        .WithMany()
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("apisHotel.Models.Hotel", "Hotel")
                        .WithMany()
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactoEmergencia");

                    b.Navigation("Habitacion");

                    b.Navigation("Hotel");
                });

            modelBuilder.Entity("apisHotel.Models.Hotel", b =>
                {
                    b.Navigation("Habitaciones");
                });

            modelBuilder.Entity("apisHotel.Models.Reserva", b =>
                {
                    b.Navigation("Huespedes");
                });
#pragma warning restore 612, 618
        }
    }
}
