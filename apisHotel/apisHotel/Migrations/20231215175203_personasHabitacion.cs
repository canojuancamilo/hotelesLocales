using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apisHotel.Migrations
{
    public partial class personasHabitacion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CantidadPersonas",
                table: "Habitaciones",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CantidadPersonas",
                table: "Habitaciones");
        }
    }
}
