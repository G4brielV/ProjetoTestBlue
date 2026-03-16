using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTestBlue.Migrations
{
    /// <inheritdoc />
    public partial class ajusteSenhaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "senha",
                value: "$2a$11$7hQ9zfoWMuNPvvlcwizuEOSvE0AKZNDS0cHWrK16Abr9PwsvtdCFG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "usuarios",
                keyColumn: "id",
                keyValue: 1,
                column: "senha",
                value: "2a$11$70cW1fGJsXB4ee8jflSaku3OIqk.znnVM21DYl.8OlrOrkrFqn.Ga");
        }
    }
}
