using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitectureExample.Persistence.Migrations
{
    public partial class addBaseToPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Person",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Person",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChange",
                table: "Person",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastChangeBy",
                table: "Person",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastChangeBy",
                table: "Person");
        }
    }
}
