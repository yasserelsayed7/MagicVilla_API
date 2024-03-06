using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magic_Villa_VillaAPI.Migrations
{
    /// <inheritdoc />
    public partial class addFKtoVilaaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "villaId",
                table: "VillaNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 14, 34, 52, 316, DateTimeKind.Local).AddTicks(9954));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 14, 34, 52, 317, DateTimeKind.Local).AddTicks(12));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 14, 34, 52, 317, DateTimeKind.Local).AddTicks(15));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 14, 34, 52, 317, DateTimeKind.Local).AddTicks(18));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 14, 34, 52, 317, DateTimeKind.Local).AddTicks(21));

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumbers_villaId",
                table: "VillaNumbers",
                column: "villaId");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumbers_Villas_villaId",
                table: "VillaNumbers",
                column: "villaId",
                principalTable: "Villas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumbers_Villas_villaId",
                table: "VillaNumbers");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumbers_villaId",
                table: "VillaNumbers");

            migrationBuilder.DropColumn(
                name: "villaId",
                table: "VillaNumbers");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 2, 22, 57, 326, DateTimeKind.Local).AddTicks(8542));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 2, 22, 57, 326, DateTimeKind.Local).AddTicks(8600));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 2, 22, 57, 326, DateTimeKind.Local).AddTicks(8603));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 2, 22, 57, 326, DateTimeKind.Local).AddTicks(8606));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2024, 3, 4, 2, 22, 57, 326, DateTimeKind.Local).AddTicks(8609));
        }
    }
}
