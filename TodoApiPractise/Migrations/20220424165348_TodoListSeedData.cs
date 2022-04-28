using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApiPractise.Migrations
{
    public partial class TodoListSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CompletedPercentage = table.Column<double>(type: "REAL", nullable: false),
                    Done = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CompletedPercentage", "Description", "Done", "EndDate", "StartDate", "Title" },
                values: new object[] { 1, 39.0, "The one with that big park.", false, new DateTime(2022, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "fhjhfjy" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CompletedPercentage", "Description", "Done", "EndDate", "StartDate", "Title" },
                values: new object[] { 2, 39.0, "The one with that big park.", false, new DateTime(2022, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "fjy" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CompletedPercentage", "Description", "Done", "EndDate", "StartDate", "Title" },
                values: new object[] { 3, 39.0, "The one with that big park.", false, new DateTime(2022, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "jy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
