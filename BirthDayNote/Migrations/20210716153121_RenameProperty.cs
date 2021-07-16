using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayNote.Migrations
{
    public partial class RenameProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoPath",
                table: "Birthdays",
                newName: "ImageName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "Birthdays",
                newName: "PhotoPath");
        }
    }
}
