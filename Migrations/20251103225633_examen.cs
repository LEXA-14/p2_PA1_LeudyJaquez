using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace p2_PA1_P2.Migrations
{
    /// <inheritdoc />
    public partial class examen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantidad",
                table: "Registro");

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "Registro",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "Registro");

            migrationBuilder.AddColumn<int>(
                name: "Cantidad",
                table: "Registro",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
