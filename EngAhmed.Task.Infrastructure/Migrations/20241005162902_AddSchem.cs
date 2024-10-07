using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EngAhmed.TaskP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSchem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Operation");

            migrationBuilder.RenameTable(
                name: "Products",
                newName: "Products",
                newSchema: "Operation");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "Customers",
                newSchema: "Operation");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Operation",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Operation",
                table: "Products");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Operation",
                newName: "Products");

            migrationBuilder.RenameTable(
                name: "Customers",
                schema: "Operation",
                newName: "Customers");
        }
    }
}
