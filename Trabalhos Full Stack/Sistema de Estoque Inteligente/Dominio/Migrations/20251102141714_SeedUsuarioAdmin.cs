using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominio.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsuarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Nome", "Senha" },
                values: new object[] { 1, "admin", "1234" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
