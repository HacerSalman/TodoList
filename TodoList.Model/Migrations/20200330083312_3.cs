using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoList.Model.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_List_ListType_type",
                table: "List");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_List",
                table: "List");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ListType",
                table: "ListType");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "user");

            migrationBuilder.RenameTable(
                name: "List",
                newName: "list");

            migrationBuilder.RenameTable(
                name: "ListType",
                newName: "list_type");

            migrationBuilder.RenameIndex(
                name: "IX_User_updated_date",
                table: "user",
                newName: "IX_user_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_User_created_date",
                table: "user",
                newName: "IX_user_created_date");

            migrationBuilder.RenameIndex(
                name: "IX_List_updated_date",
                table: "list",
                newName: "IX_list_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_List_type",
                table: "list",
                newName: "IX_list_type");

            migrationBuilder.RenameIndex(
                name: "IX_List_created_date",
                table: "list",
                newName: "IX_list_created_date");

            migrationBuilder.RenameIndex(
                name: "IX_ListType_updated_date",
                table: "list_type",
                newName: "IX_list_type_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_ListType_name",
                table: "list_type",
                newName: "IX_list_type_name");

            migrationBuilder.RenameIndex(
                name: "IX_ListType_created_date",
                table: "list_type",
                newName: "IX_list_type_created_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user",
                table: "user",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_list",
                table: "list",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_list_type",
                table: "list_type",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_list_list_type_type",
                table: "list",
                column: "type",
                principalTable: "list_type",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_list_list_type_type",
                table: "list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user",
                table: "user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_list",
                table: "list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_list_type",
                table: "list_type");

            migrationBuilder.RenameTable(
                name: "user",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "list",
                newName: "List");

            migrationBuilder.RenameTable(
                name: "list_type",
                newName: "ListType");

            migrationBuilder.RenameIndex(
                name: "IX_user_updated_date",
                table: "User",
                newName: "IX_User_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_user_created_date",
                table: "User",
                newName: "IX_User_created_date");

            migrationBuilder.RenameIndex(
                name: "IX_list_updated_date",
                table: "List",
                newName: "IX_List_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_list_type",
                table: "List",
                newName: "IX_List_type");

            migrationBuilder.RenameIndex(
                name: "IX_list_created_date",
                table: "List",
                newName: "IX_List_created_date");

            migrationBuilder.RenameIndex(
                name: "IX_list_type_updated_date",
                table: "ListType",
                newName: "IX_ListType_updated_date");

            migrationBuilder.RenameIndex(
                name: "IX_list_type_name",
                table: "ListType",
                newName: "IX_ListType_name");

            migrationBuilder.RenameIndex(
                name: "IX_list_type_created_date",
                table: "ListType",
                newName: "IX_ListType_created_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_List",
                table: "List",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ListType",
                table: "ListType",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_List_ListType_type",
                table: "List",
                column: "type",
                principalTable: "ListType",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
