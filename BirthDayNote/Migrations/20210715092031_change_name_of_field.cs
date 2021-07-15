using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayNote.Migrations
{
    public partial class change_name_of_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthdayTime",
                table: "Birthdays",
                newName: "BirthdayDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthdayDate",
                table: "Birthdays",
                newName: "BirthdayTime");
        }
    }
}
