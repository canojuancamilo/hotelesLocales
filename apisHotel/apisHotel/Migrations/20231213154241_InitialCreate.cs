using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apisHotel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactosEmergencia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactosEmergencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hoteles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Habilitado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hoteles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habitaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostoBase = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Impuestos = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Habilitada = table.Column<bool>(type: "bit", nullable: false),
                    HotelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitaciones_Hoteles_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelId = table.Column<int>(type: "int", nullable: false),
                    HabitacionId = table.Column<int>(type: "int", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CantidadPersonas = table.Column<int>(type: "int", nullable: false),
                    ContactoEmergenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservas_ContactosEmergencia_ContactoEmergenciaId",
                        column: x => x.ContactoEmergenciaId,
                        principalTable: "ContactosEmergencia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Habitaciones_HabitacionId",
                        column: x => x.HabitacionId,
                        principalTable: "Habitaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Hoteles_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hoteles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Huespedes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumeroDocumento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefonoContacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReservaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Huespedes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huespedes_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitaciones_HotelId",
                table: "Habitaciones",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Huespedes_ReservaId",
                table: "Huespedes",
                column: "ReservaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_ContactoEmergenciaId",
                table: "Reservas",
                column: "ContactoEmergenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HabitacionId",
                table: "Reservas",
                column: "HabitacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HotelId",
                table: "Reservas",
                column: "HotelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Huespedes");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "ContactosEmergencia");

            migrationBuilder.DropTable(
                name: "Habitaciones");

            migrationBuilder.DropTable(
                name: "Hoteles");
        }
    }
}
