using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitectureExample.Persistence.Migrations
{
    public partial class PersonEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PersonPhone");

            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "PersonPhone");

            migrationBuilder.DropColumn(
                name: "LastChangeBy",
                table: "PersonPhone");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "LastChangeBy",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "LastChange",
                table: "Author");

            migrationBuilder.DropColumn(
                name: "LastChangeBy",
                table: "Author");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Person",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Person");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "PersonPhone",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChange",
                table: "PersonPhone",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastChangeBy",
                table: "PersonPhone",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Person",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChange",
                table: "Person",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastChangeBy",
                table: "Person",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Author",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastChange",
                table: "Author",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "LastChangeBy",
                table: "Author",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
