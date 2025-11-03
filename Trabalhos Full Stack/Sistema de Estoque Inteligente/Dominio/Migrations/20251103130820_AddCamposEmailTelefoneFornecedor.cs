using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dominio.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposEmailTelefoneFornecedor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Contato",
                table: "Fornecedores",
                newName: "Telefone");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Fornecedores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Fornecedores");

            migrationBuilder.RenameColumn(
                name: "Telefone",
                table: "Fornecedores",
                newName: "Contato");
        }
    }
}
