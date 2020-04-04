using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_list_type_name",
                table: "list_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_list_type_name",
                table: "list_type",
                column: "name",
                unique: true);
        }
    }
}
