using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flavory.Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTwoFieldsInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderHeaders");
        }
    }
}
