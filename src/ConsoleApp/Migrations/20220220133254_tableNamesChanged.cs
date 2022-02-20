using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    public partial class tableNamesChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "students",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "students",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "student",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                });
        }
    }
}
