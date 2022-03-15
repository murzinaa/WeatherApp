using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperature",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    Degrees = table.Column<double>(type: "float", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsArchieved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Temperature_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Kyiv" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Lviv" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Kharkiv" });

            migrationBuilder.InsertData(
                table: "Temperature",
                columns: new[] { "Id", "CityId", "DateTime", "Degrees", "IsArchieved" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 3, 15, 11, 39, 5, 208, DateTimeKind.Local).AddTicks(3580), 12.0, false },
                    { 3, 2, new DateTime(2022, 3, 14, 13, 30, 30, 0, DateTimeKind.Unspecified), -5.0, false },
                    { 4, 2, new DateTime(2022, 3, 15, 9, 20, 59, 0, DateTimeKind.Unspecified), 10.0, false },
                    { 2, 3, new DateTime(2022, 3, 14, 12, 2, 30, 0, DateTimeKind.Unspecified), 0.0, false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Temperature_CityId",
                table: "Temperature",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Temperature");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
