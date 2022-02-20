using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    public partial class tableAndColumnNameChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_students",
                schema: "dbo",
                table: "students");

            migrationBuilder.RenameTable(
                name: "students",
                schema: "dbo",
                newName: "student",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "student",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "dbo",
                table: "student",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "dbo",
                table: "student",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "dbo",
                table: "student",
                newName: "birth_date");

            migrationBuilder.AddPrimaryKey(
                name: "PK_student",
                schema: "dbo",
                table: "student",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_student",
                schema: "dbo",
                table: "student");

            migrationBuilder.RenameTable(
                name: "student",
                schema: "dbo",
                newName: "students",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "dbo",
                table: "students",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "dbo",
                table: "students",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                schema: "dbo",
                table: "students",
                newName: "BirthDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_students",
                schema: "dbo",
                table: "students",
                column: "Id");
        }
    }
}
