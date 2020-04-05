using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_list_type",
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
                    list_type_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_list_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_list_type_list_type_list_type_id",
                        column: x => x.list_type_id,
                        principalTable: "list_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_user_list_type_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_list_type_created_date",
                table: "user_list_type",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_type_list_type_id",
                table: "user_list_type",
                column: "list_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_type_updated_date",
                table: "user_list_type",
                column: "updated_date");

            migrationBuilder.CreateIndex(
                name: "IX_user_list_type_user_id_list_type_id",
                table: "user_list_type",
                columns: new[] { "user_id", "list_type_id" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_list_type");
        }
    }
}
