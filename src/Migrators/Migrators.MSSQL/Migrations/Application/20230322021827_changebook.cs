using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class changebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_BookRecords_BookId",
                schema: "Catalog",
                table: "BookRecords",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookRecords_Books_BookId",
                schema: "Catalog",
                table: "BookRecords",
                column: "BookId",
                principalSchema: "Catalog",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRecords_Books_BookId",
                schema: "Catalog",
                table: "BookRecords");

            migrationBuilder.DropIndex(
                name: "IX_BookRecords_BookId",
                schema: "Catalog",
                table: "BookRecords");
        }
    }
}
