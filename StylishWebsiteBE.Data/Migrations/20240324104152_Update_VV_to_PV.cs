using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_VV_to_PV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantReadModelId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductVariantReadModelId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "ProductVariantReadModelId",
                table: "VariantValues");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantId",
                table: "VariantValues",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantId",
                table: "VariantValues");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantReadModelId",
                table: "VariantValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductVariantReadModelId",
                table: "VariantValues",
                column: "ProductVariantReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantReadModelId",
                table: "VariantValues",
                column: "ProductVariantReadModelId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }
    }
}
