using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeatherApp.DataLayer.Migrations
{
    public partial class edits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Temperature_Cities_CityId",
                table: "Temperature");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature");

            migrationBuilder.RenameTable(
                name: "Temperature",
                newName: "WeatherConditions");

            migrationBuilder.RenameIndex(
                name: "IX_Temperature_CityId",
                table: "WeatherConditions",
                newName: "IX_WeatherConditions_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeatherConditions",
                table: "WeatherConditions",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "WeatherConditions",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 3, 16, 15, 5, 28, 843, DateTimeKind.Local).AddTicks(1921));

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherConditions_Cities_CityId",
                table: "WeatherConditions",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherConditions_Cities_CityId",
                table: "WeatherConditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeatherConditions",
                table: "WeatherConditions");

            migrationBuilder.RenameTable(
                name: "WeatherConditions",
                newName: "Temperature");

            migrationBuilder.RenameIndex(
                name: "IX_WeatherConditions_CityId",
                table: "Temperature",
                newName: "IX_Temperature_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Temperature",
                table: "Temperature",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Temperature",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateTime",
                value: new DateTime(2022, 3, 16, 14, 55, 35, 168, DateTimeKind.Local).AddTicks(6369));

            migrationBuilder.AddForeignKey(
                name: "FK_Temperature_Cities_CityId",
                table: "Temperature",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
