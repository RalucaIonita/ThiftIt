using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class smth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("11dd2219-90d0-4c59-b484-9fc86d6ba515"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ebdde7c8-13bd-44bb-8f52-fcd54abafe46"));

            migrationBuilder.DropColumn(
                name: "Reserved",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProductId",
                table: "Pictures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Products_ProductId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProductId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Pictures");

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("11dd2219-90d0-4c59-b484-9fc86d6ba515"), "18fec9de-20e6-4ec4-a415-d90628bd3cd0", "BasicUser", "BASICUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("ebdde7c8-13bd-44bb-8f52-fcd54abafe46"), "595c5875-2979-43f9-851a-b0d1cd9153ee", "SuperAdmin", "SUPERADMIN" });
        }
    }
}
