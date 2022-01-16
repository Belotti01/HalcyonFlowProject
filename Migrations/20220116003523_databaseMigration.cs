using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HalcyonFlowProject.Migrations
{
    public partial class databaseMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "fk_role",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "is_default_role",
                table: "AspNetUserRoles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fk_role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "is_default_role",
                table: "AspNetUserRoles");
        }
    }
}
