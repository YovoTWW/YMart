using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YMart.Data.Migrations
{
    /// <inheritdoc />
    public partial class changedBrochure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Brochure_BrochureId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BrochureId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BrochureId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ProductNames",
                table: "Brochure",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductNames",
                table: "Brochure");

            migrationBuilder.AddColumn<Guid>(
                name: "BrochureId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

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
    }
}
