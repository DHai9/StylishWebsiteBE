using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariantOptionValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionValues_ProductVariants_ProductVariantReadModelId",
                table: "OptionValues");

            migrationBuilder.DropIndex(
                name: "IX_OptionValues_ProductVariantReadModelId",
                table: "OptionValues");

            migrationBuilder.DropColumn(
                name: "ProductVariantReadModelId",
                table: "OptionValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantReadModelId",
                table: "OptionValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionValues_ProductVariantReadModelId",
                table: "OptionValues",
                column: "ProductVariantReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionValues_ProductVariants_ProductVariantReadModelId",
                table: "OptionValues",
                column: "ProductVariantReadModelId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }
    }
}
