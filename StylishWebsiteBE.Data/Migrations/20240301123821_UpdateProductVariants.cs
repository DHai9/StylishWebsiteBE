using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValuesReadModel_OptionValues_ProductId",
                table: "VariantValuesReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValuesReadModel_Options_ProductId",
                table: "VariantValuesReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValuesReadModel_ProductOptions_ProductId",
                table: "VariantValuesReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValuesReadModel_ProductVariants_ProductVariantId",
                table: "VariantValuesReadModel");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValuesReadModel_Products_ProductId",
                table: "VariantValuesReadModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantValuesReadModel",
                table: "VariantValuesReadModel");

            migrationBuilder.RenameTable(
                name: "VariantValuesReadModel",
                newName: "VariantValues");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValuesReadModel_ProductVariantId",
                table: "VariantValues",
                newName: "IX_VariantValues_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValuesReadModel_ProductId",
                table: "VariantValues",
                newName: "IX_VariantValues_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValuesReadModel_Id",
                table: "VariantValues",
                newName: "IX_VariantValues_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantValues",
                table: "VariantValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_OptionValues_ProductId",
                table: "VariantValues",
                column: "ProductId",
                principalTable: "OptionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_ProductId",
                table: "VariantValues",
                column: "ProductId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductId",
                table: "VariantValues",
                column: "ProductId",
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

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Products_ProductId",
                table: "VariantValues",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_OptionValues_ProductId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_ProductId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductOptions_ProductId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_ProductVariants_ProductVariantId",
                table: "VariantValues");

            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Products_ProductId",
                table: "VariantValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VariantValues",
                table: "VariantValues");

            migrationBuilder.RenameTable(
                name: "VariantValues",
                newName: "VariantValuesReadModel");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValues_ProductVariantId",
                table: "VariantValuesReadModel",
                newName: "IX_VariantValuesReadModel_ProductVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValues_ProductId",
                table: "VariantValuesReadModel",
                newName: "IX_VariantValuesReadModel_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_VariantValues_Id",
                table: "VariantValuesReadModel",
                newName: "IX_VariantValuesReadModel_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VariantValuesReadModel",
                table: "VariantValuesReadModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValuesReadModel_OptionValues_ProductId",
                table: "VariantValuesReadModel",
                column: "ProductId",
                principalTable: "OptionValues",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValuesReadModel_Options_ProductId",
                table: "VariantValuesReadModel",
                column: "ProductId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValuesReadModel_ProductOptions_ProductId",
                table: "VariantValuesReadModel",
                column: "ProductId",
                principalTable: "ProductOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValuesReadModel_ProductVariants_ProductVariantId",
                table: "VariantValuesReadModel",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValuesReadModel_Products_ProductId",
                table: "VariantValuesReadModel",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
