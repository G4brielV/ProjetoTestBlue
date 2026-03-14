using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoTestBlue.Migrations
{
    /// <inheritdoc />
    public partial class FK_snake_case : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_todo_lists_ListId",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_todo_lists_usuarios_UsuarioId",
                table: "todo_lists");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "todo_lists",
                newName: "usuario_id");

            migrationBuilder.RenameIndex(
                name: "IX_todo_lists_UsuarioId",
                table: "todo_lists",
                newName: "IX_todo_lists_usuario_id");

            migrationBuilder.RenameColumn(
                name: "ListId",
                table: "cards",
                newName: "list_id");

            migrationBuilder.RenameIndex(
                name: "IX_cards_ListId",
                table: "cards",
                newName: "IX_cards_list_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_todo_lists_list_id",
                table: "cards",
                column: "list_id",
                principalTable: "todo_lists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_todo_lists_usuarios_usuario_id",
                table: "todo_lists",
                column: "usuario_id",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cards_todo_lists_list_id",
                table: "cards");

            migrationBuilder.DropForeignKey(
                name: "FK_todo_lists_usuarios_usuario_id",
                table: "todo_lists");

            migrationBuilder.RenameColumn(
                name: "usuario_id",
                table: "todo_lists",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_todo_lists_usuario_id",
                table: "todo_lists",
                newName: "IX_todo_lists_UsuarioId");

            migrationBuilder.RenameColumn(
                name: "list_id",
                table: "cards",
                newName: "ListId");

            migrationBuilder.RenameIndex(
                name: "IX_cards_list_id",
                table: "cards",
                newName: "IX_cards_ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_cards_todo_lists_ListId",
                table: "cards",
                column: "ListId",
                principalTable: "todo_lists",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_todo_lists_usuarios_UsuarioId",
                table: "todo_lists",
                column: "UsuarioId",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
