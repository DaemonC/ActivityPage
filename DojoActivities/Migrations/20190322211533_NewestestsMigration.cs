using Microsoft.EntityFrameworkCore.Migrations;

namespace DojoActivities.Migrations
{
    public partial class NewestestsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationUnit",
                table: "Activs",
                newName: "DuratUnit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DuratUnit",
                table: "Activs",
                newName: "DurationUnit");
        }
    }
}
