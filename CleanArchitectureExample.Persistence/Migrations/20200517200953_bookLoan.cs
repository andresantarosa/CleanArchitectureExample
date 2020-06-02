using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitectureExample.Persistence.Migrations
{
    public partial class bookLoan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Book",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Book",
                type: "varchar(13)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookSituation",
                table: "Book",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookLoan",
                columns: table => new
                {
                    BookLoanId = table.Column<Guid>(nullable: false),
                    BookId = table.Column<Guid>(nullable: false),
                    TakerId = table.Column<Guid>(nullable: false),
                    CheckoutDate = table.Column<DateTime>(nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(nullable: false),
                    ActualReturnDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLoan", x => x.BookLoanId);
                    table.ForeignKey(
                        name: "FK_BookLoan_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookLoan_Person_TakerId",
                        column: x => x.TakerId,
                        principalTable: "Person",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLoan_BookId",
                table: "BookLoan",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLoan_TakerId",
                table: "BookLoan",
                column: "TakerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLoan");

            migrationBuilder.DropColumn(
                name: "BookSituation",
                table: "Book");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)");

            migrationBuilder.AlterColumn<string>(
                name: "ISBN",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(13)");
        }
    }
}
