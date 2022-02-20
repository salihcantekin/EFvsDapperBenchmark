using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    public partial class columnNamesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_teacher",
                schema: "dbo",
                table: "teacher");

            migrationBuilder.DropPrimaryKey(
                name: "PK_course",
                schema: "dbo",
                table: "course");

            migrationBuilder.RenameTable(
                name: "teacher",
                schema: "dbo",
                newName: "teachers",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "course",
                schema: "dbo",
                newName: "courses",
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

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "teachers",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "dbo",
                table: "teachers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "dbo",
                table: "teachers",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                schema: "dbo",
                table: "teachers",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "dbo",
                table: "courses",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "dbo",
                table: "courses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "dbo",
                table: "courses",
                newName: "IsActive");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "dbo",
                table: "students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "students",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                schema: "dbo",
                table: "teachers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                schema: "dbo",
                table: "teachers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "courses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_teachers",
                schema: "dbo",
                table: "teachers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                schema: "dbo",
                table: "courses",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_teachers",
                schema: "dbo",
                table: "teachers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                schema: "dbo",
                table: "courses");

            migrationBuilder.RenameTable(
                name: "teachers",
                schema: "dbo",
                newName: "teacher",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "courses",
                schema: "dbo",
                newName: "course",
                newSchema: "dbo");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "students",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "dbo",
                table: "students",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "dbo",
                table: "students",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "dbo",
                table: "students",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "teacher",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "dbo",
                table: "teacher",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "dbo",
                table: "teacher",
                newName: "first_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                schema: "dbo",
                table: "teacher",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "dbo",
                table: "course",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                schema: "dbo",
                table: "course",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "dbo",
                table: "course",
                newName: "is_active");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "dbo",
                table: "students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "dbo",
                table: "students",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                schema: "dbo",
                table: "teacher",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "first_name",
                schema: "dbo",
                table: "teacher",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "dbo",
                table: "course",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_teacher",
                schema: "dbo",
                table: "teacher",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_course",
                schema: "dbo",
                table: "course",
                column: "id");
        }
    }
}
