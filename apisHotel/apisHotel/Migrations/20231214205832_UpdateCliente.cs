using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apisHotel.Migrations
{
    public partial class UpdateCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genero",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TipoDocumento",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genero",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TipoDocumento",
                table: "AspNetUsers");
        }
    }
}
