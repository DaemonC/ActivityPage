using Microsoft.EntityFrameworkCore.Migrations;

namespace DojoActivities.Migrations
{
    public partial class NewestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Activs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DurationUnit",
                table: "Activs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Activs");

            migrationBuilder.DropColumn(
                name: "DurationUnit",
                table: "Activs");
        }
    }
}
