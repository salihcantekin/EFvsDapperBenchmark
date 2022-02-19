using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleApp.Migrations
{
    public partial class initMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                schema: "efdapperbenchmark",
                table: "student",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "efdapperbenchmark",
                table: "student",
                newName: "first_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "last_name",
                schema: "efdapperbenchmark",
                table: "student",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "efdapperbenchmark",
                table: "student",
                newName: "FirstName");
        }
    }
}
