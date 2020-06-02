using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitectureExample.Persistence.Migrations
{
    public partial class Recreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PersonPhone",
                columns: table => new
                {
                    PersonPhoneId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    LastChange = table.Column<DateTime>(nullable: false),
                    LastChangeBy = table.Column<Guid>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonPhone", x => x.PersonPhoneId);
                    table.ForeignKey(
                        name: "FK_PersonPhone_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonPhone_PersonId",
                table: "PersonPhone",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonPhone");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
