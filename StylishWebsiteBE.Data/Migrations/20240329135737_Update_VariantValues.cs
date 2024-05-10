using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class Update_VariantValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues",
                column: "OptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VariantValues_Options_OptionId",
                table: "VariantValues",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VariantValues_Options_OptionId",
                table: "VariantValues");

            migrationBuilder.DropIndex(
                name: "IX_VariantValues_OptionId",
                table: "VariantValues");
        }
    }
}
