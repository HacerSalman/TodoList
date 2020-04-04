using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_list",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_date = table.Column<long>(nullable: false),
                    updated_date = table.Column<long>(nullable: false),
                    owner_by = table.Column<string>(maxLength: 80, nullable: true),
                    modifier_by = table.Column<string>(maxLength: 80, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    list_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_list_list_list_id",
                        column: x => x.list_id,
                        principalTable: "list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_list_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_list_created_date",
                table: "user_list",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_list_id",
                table: "user_list",
                column: "list_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_updated_date",
                table: "user_list",
                column: "updated_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_user_id",
                table: "user_list",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_list");
        }
    }
}
