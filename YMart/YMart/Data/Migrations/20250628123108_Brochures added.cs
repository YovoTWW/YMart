using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YMart.Data.Migrations
{
    /// <inheritdoc />
    public partial class Brochuresadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BrochureId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brochure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brochure", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrochureId",
                table: "Products",
                column: "BrochureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Brochure_BrochureId",
                table: "Products",
                column: "BrochureId",
                principalTable: "Brochure",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brochure_BrochureId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Brochure");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrochureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrochureId",
                table: "Products");
        }
    }
}
