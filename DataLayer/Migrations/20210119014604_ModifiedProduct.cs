using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ModifiedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("c916942b-a206-4bfd-9357-06d329e30fe4"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("cb130b5c-3853-4c3d-9c0b-e0050ff041a9"));

            migrationBuilder.AddColumn<bool>(
                name: "Reserved",
                table: "Products",
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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("c916942b-a206-4bfd-9357-06d329e30fe4"), "3e34513b-f0cd-47ff-a65c-8cff2c4a64a0", "BasicUser", "BASICUSER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("cb130b5c-3853-4c3d-9c0b-e0050ff041a9"), "53c58ae0-0804-4e7e-bebb-30777b7b53c1", "SuperAdmin", "SUPERADMIN" });
        }
    }
}
