using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFK_VV : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_OptionValues_ValueId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_OptionId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductOptionId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ValueId",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductOptionId",
                table: "VariantValues",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ValueId",
                table: "VariantValues",
                column: "ValueId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_OptionValues_ValueId",
                table: "VariantValues",
                column: "ValueId",
                principalTable: "OptionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_OptionId",
                table: "VariantValues",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductOptionId",
                table: "VariantValues",
                column: "ProductOptionId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
