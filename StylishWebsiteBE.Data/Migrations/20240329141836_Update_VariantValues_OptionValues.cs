using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_VariantValues_OptionValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_OptionValues_ValueId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_ValueId",
                table: "VariantValues");
        }
    }
}
