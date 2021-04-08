using Microsoft.EntityFrameworkCore.Migrations;

namespace FurnitureFactoryDatabaseImplement.Migrations
{
    public partial class newmigr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "FurnitureModels");

            migrationBuilder.AddColumn<string>(
                name: "TypeName",
                table: "FurnitureModels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeName",
                table: "FurnitureModels");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "FurnitureModels",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
