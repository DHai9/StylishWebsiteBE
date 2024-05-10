using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_VV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_OptionValues_OptionValuesId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_OptionsId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionsId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionsId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionValuesId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductOptionsId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "OptionValuesId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "OptionsId",
                table: "VariantValues");

            migrationBuilder.DropColumn(
                name: "ProductOptionsId",
                table: "VariantValues");

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

            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantReadModelId",
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

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductVariantReadModelId",
                table: "VariantValues",
                column: "ProductVariantReadModelId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantReadModelId",
                table: "VariantValues",
                column: "ProductVariantReadModelId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantReadModelId",
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

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductVariantReadModelId",
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

            migrationBuilder.DropColumn(
                name: "ProductVariantReadModelId",
                table: "VariantValues");

            migrationBuilder.AddColumn<Guid>(
                name: "OptionValuesId",
                table: "VariantValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OptionsId",
                table: "VariantValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductOptionsId",
                table: "VariantValues",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionsId",
                table: "VariantValues",
                column: "OptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionValuesId",
                table: "VariantValues",
                column: "OptionValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductOptionsId",
                table: "VariantValues",
                column: "ProductOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_OptionValues_OptionValuesId",
                table: "VariantValues",
                column: "OptionValuesId",
                principalTable: "OptionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_OptionsId",
                table: "VariantValues",
                column: "OptionsId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionsId",
                table: "VariantValues",
                column: "ProductOptionsId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantId",
                table: "VariantValues",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
