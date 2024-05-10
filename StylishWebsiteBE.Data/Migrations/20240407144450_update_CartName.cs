using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_CartName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Carts_CardId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_Id",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "CartDetails",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CardId",
                table: "CartDetails",
                newName: "IX_CartDetails_CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_Id",
                table: "CartDetails",
                column: "Id",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "CartId", "ProductVariantId", "Quantity" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Carts_CartId",
                table: "CartDetails",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Carts_CartId",
                table: "CartDetails");

            migrationBuilder.DropIndex(
                name: "IX_CartDetails_Id",
                table: "CartDetails");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartDetails",
                newName: "CardId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetails_CartId",
                table: "CartDetails",
                newName: "IX_CartDetails_CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_Id",
                table: "CartDetails",
                column: "Id",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "CardId", "ProductVariantId", "Quantity" });

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Carts_CardId",
                table: "CartDetails",
                column: "CardId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
