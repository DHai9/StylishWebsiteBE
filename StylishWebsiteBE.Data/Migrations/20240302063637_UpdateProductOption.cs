using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Products_ProductReadModelId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ProductReadModelId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ProductReadModelId",
                table: "Options");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductReadModelId",
                table: "Options",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductReadModelId",
                table: "Options",
                column: "ProductReadModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Products_ProductReadModelId",
                table: "Options",
                column: "ProductReadModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
