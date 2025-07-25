using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YMart.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedorders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemPrices",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemPrices",
                table: "Orders");
        }
    }
}
