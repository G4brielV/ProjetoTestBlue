using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTestBlue.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "usuarios",
                columns: new[] { "id", "email", "nome", "senha" },
                values: new object[] { 1, "teste@example.com", "Teste", "2a$11$70cW1fGJsXB4ee8jflSaku3OIqk.znnVM21DYl.8OlrOrkrFqn.Ga" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: 1);
        }
    }
}
