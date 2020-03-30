using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListType",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_date = table.Column<long>(nullable: false),
                    updated_date = table.Column<long>(nullable: false),
                    deleted_date = table.Column<long>(nullable: true),
                    owner_by = table.Column<string>(maxLength: 80, nullable: true),
                    modifier_by = table.Column<string>(maxLength: 80, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_date = table.Column<long>(nullable: false),
                    updated_date = table.Column<long>(nullable: false),
                    deleted_date = table.Column<long>(nullable: true),
                    owner_by = table.Column<string>(maxLength: 80, nullable: true),
                    modifier_by = table.Column<string>(maxLength: 80, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    user_name = table.Column<string>(maxLength: 80, nullable: false),
                    full_name = table.Column<string>(maxLength: 80, nullable: false),
                    password = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "List",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_date = table.Column<long>(nullable: false),
                    updated_date = table.Column<long>(nullable: false),
                    deleted_date = table.Column<long>(nullable: true),
                    owner_by = table.Column<string>(maxLength: 80, nullable: true),
                    modifier_by = table.Column<string>(maxLength: 80, nullable: true),
                    status = table.Column<byte>(nullable: true),
                    title = table.Column<string>(maxLength: 200, nullable: false),
                    description = table.Column<string>(maxLength: 5000, nullable: false),
                    starts_at = table.Column<long>(nullable: false),
                    ends_at = table.Column<long>(nullable: true),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List", x => x.id);
                    table.ForeignKey(
                        name: "FK_List_ListType_type",
                        column: x => x.type,
                        principalTable: "ListType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_List_created_date",
                table: "List",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_List_type",
                table: "List",
                column: "type");

            migrationBuilder.CreateIndex(
                name: "IX_List_updated_date",
                table: "List",
                column: "updated_date");

            migrationBuilder.CreateIndex(
                name: "IX_ListType_created_date",
                table: "ListType",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_ListType_name",
                table: "ListType",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ListType_updated_date",
                table: "ListType",
                column: "updated_date");

            migrationBuilder.CreateIndex(
                name: "IX_User_created_date",
                table: "User",
                column: "created_date");

            migrationBuilder.CreateIndex(
                name: "IX_User_updated_date",
                table: "User",
                column: "updated_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "List");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ListType");
        }
    }
}
