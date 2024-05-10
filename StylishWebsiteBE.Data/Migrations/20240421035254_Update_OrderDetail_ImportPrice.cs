using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_OrderDetail_ImportPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ImportPrice",
                table: "OrderDetails",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImportPrice",
                table: "OrderDetails");
        }
    }
}
