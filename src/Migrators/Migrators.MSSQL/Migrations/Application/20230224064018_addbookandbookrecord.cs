using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class addbookandbookrecord : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookRecords",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookRecordType = table.Column<int>(type: "int", nullable: false),
                    CreateUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BookType = table.Column<int>(type: "int", nullable: false),
                    IsBorrowed = table.Column<bool>(type: "bit", nullable: false),
                    BookShelfLayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookShelfLayers_BookShelfLayerId",
                        column: x => x.BookShelfLayerId,
                        principalSchema: "Catalog",
                        principalTable: "BookShelfLayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookShelfLayerId",
                schema: "Catalog",
                table: "Books",
                column: "BookShelfLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Name_IsBorrowed_Author_BookType",
                schema: "Catalog",
                table: "Books",
                columns: new[] { "Name", "IsBorrowed", "Author", "BookType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookRecords",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Books",
                schema: "Catalog");
        }
    }
}
