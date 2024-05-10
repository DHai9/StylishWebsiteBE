using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Products_ProductId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_ProductId",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "Options");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Options");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "Options",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId",
                table: "Options",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Options_ProductId",
                table: "Options",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Products_ProductId",
                table: "Options",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
