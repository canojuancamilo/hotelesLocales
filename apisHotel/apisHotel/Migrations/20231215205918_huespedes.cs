using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apisHotel.Migrations
{
    public partial class huespedes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Reservas_ReservaId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservas_Hoteles_HotelId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_Reservas_HotelId",
                table: "Reservas");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ReservaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "ReservaId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Huesped",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresApellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_Huesped", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Huesped_Reservas_ReservaId",
                        column: x => x.ReservaId,
                        principalTable: "Reservas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Huesped_ReservaId",
                table: "Huesped",
                column: "ReservaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Huesped");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "Reservas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReservaId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_HotelId",
                table: "Reservas",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ReservaId",
                table: "AspNetUsers",
                column: "ReservaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Reservas_ReservaId",
                table: "AspNetUsers",
                column: "ReservaId",
                principalTable: "Reservas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservas_Hoteles_HotelId",
                table: "Reservas",
                column: "HotelId",
                principalTable: "Hoteles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
