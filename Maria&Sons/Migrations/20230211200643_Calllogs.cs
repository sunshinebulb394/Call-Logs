using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maria_Sons.Migrations
{
    public partial class Calllogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallLogs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CallDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CallerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostOfCall = table.Column<double>(type: "float", nullable: false),
                    CallType = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallLogs", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallLogs");
        }
    }
}
