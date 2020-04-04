using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deleted_date",
                table: "user");

            migrationBuilder.DropColumn(
                name: "deleted_date",
                table: "list_type");

            migrationBuilder.DropColumn(
                name: "deleted_date",
                table: "list");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "deleted_date",
                table: "user",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "deleted_date",
                table: "list_type",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "deleted_date",
                table: "list",
                type: "bigint",
                nullable: true);
        }
    }
}
