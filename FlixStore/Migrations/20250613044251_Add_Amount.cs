using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlixStore.Migrations
{
    /// <inheritdoc />
    public partial class Add_Amount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amout",
                table: "ShoppingCartItems",
                newName: "Amount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ShoppingCartItems",
                newName: "Amout");
        }
    }
}
