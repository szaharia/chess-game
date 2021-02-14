using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChessGame.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WhitePlayerId = table.Column<int>(nullable: true),
                    BlackPlayerId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    OpeningClassification = table.Column<string>(maxLength: 3, nullable: false),
                    Result = table.Column<string>(type: "nvarchar(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_BlackPlayerId",
                        column: x => x.BlackPlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Games_Players_WhitePlayerId",
                        column: x => x.WhitePlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "LastName", "Rating" },
                values: new object[] { 1, "John", "Smith", 100 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "LastName", "Rating" },
                values: new object[] { 2, "Joanne", "Doe", 200 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "LastName", "Rating" },
                values: new object[] { 3, "Silviu", "Zaharia", 200 });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "BlackPlayerId", "Date", "OpeningClassification", "Result", "WhitePlayerId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2021, 2, 5, 1, 31, 42, 643, DateTimeKind.Local).AddTicks(5374), "A10", "1-0", 1 },
                    { 4, 2, new DateTime(2021, 2, 6, 1, 31, 42, 646, DateTimeKind.Local).AddTicks(7764), "F17", "1-0", 1 },
                    { 5, 2, new DateTime(2021, 2, 15, 1, 31, 42, 646, DateTimeKind.Local).AddTicks(7806), "B17", "1-0", 2 },
                    { 2, 3, new DateTime(2021, 2, 5, 1, 31, 42, 646, DateTimeKind.Local).AddTicks(7547), "B13", "1-0", 1 },
                    { 3, 3, new DateTime(2021, 2, 6, 1, 31, 42, 646, DateTimeKind.Local).AddTicks(7709), "E03", "0.5-0.5", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_BlackPlayerId",
                table: "Games",
                column: "BlackPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_WhitePlayerId",
                table: "Games",
                column: "WhitePlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
