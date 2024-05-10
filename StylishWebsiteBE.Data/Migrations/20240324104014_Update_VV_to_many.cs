using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_VV_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_OptionValues_OptionValueReadModelId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_OptionReadModelId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionReadModelId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionReadModelId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionValueReadModelId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductOptionReadModelId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "OptionReadModelId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "OptionValueReadModelId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "ProductOptionReadModelId",
                table: "VariantValues");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OptionReadModelId",
                table: "VariantValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OptionValueReadModelId",
                table: "VariantValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductOptionReadModelId",
                table: "VariantValues",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionReadModelId",
                table: "VariantValues",
                column: "OptionReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionValueReadModelId",
                table: "VariantValues",
                column: "OptionValueReadModelId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductOptionReadModelId",
                table: "VariantValues",
                column: "ProductOptionReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_OptionValues_OptionValueReadModelId",
                table: "VariantValues",
                column: "OptionValueReadModelId",
                principalTable: "OptionValues",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_OptionReadModelId",
                table: "VariantValues",
                column: "OptionReadModelId",
                principalTable: "Options",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionReadModelId",
                table: "VariantValues",
                column: "ProductOptionReadModelId",
                principalTable: "ProductOptions",
                principalColumn: "Id");
        }
    }
}
