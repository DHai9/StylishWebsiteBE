using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVariantValueFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductVariantId",
                table: "VariantValues");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductId",
                table: "VariantValues",
                column: "ProductId")
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductVariantId", "ValueId" });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductOptionId",
                table: "VariantValues",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductVariantId",
                table: "VariantValues",
                column: "ProductVariantId")
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductId", "ValueId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "IX_VariantValues_ProductId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductOptionId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ProductVariantId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ValueId",
                table: "VariantValues");

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductId",
                table: "VariantValues",
                column: "ProductId",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductVariantId", "ValueId" });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_ProductVariantId",
                table: "VariantValues",
                column: "ProductVariantId",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductId", "ValueId" });

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
        }
    }
}
