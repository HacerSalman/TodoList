using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_list_list_list_id",
                table: "user_list");

            migrationBuilder.DropForeignKey(
                name: "FK_user_list_user_user_id",
                table: "user_list");

            migrationBuilder.DropIndex(
                name: "IX_user_list_created_date",
                table: "user_list");

            migrationBuilder.DropIndex(
                name: "IX_user_list_updated_date",
                table: "user_list");

            migrationBuilder.AddForeignKey(
                name: "FK_user_list_list_list_id",
                table: "user_list",
                column: "list_id",
                principalTable: "list",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_list_user_user_id",
                table: "user_list",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_user_list_list_list_id",
                table: "user_list");

            migrationBuilder.DropForeignKey(
                name: "FK_user_list_user_user_id",
                table: "user_list");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_created_date",
                table: "user_list",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_updated_date",
                table: "user_list",
                column: "updated_date");

            migrationBuilder.AddForeignKey(
                name: "FK_user_list_list_list_id",
                table: "user_list",
                column: "list_id",
                principalTable: "list",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_user_list_user_user_id",
                table: "user_list",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
