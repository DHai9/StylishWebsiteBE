using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StylishWebsiteBE.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductVariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VariantValuesReadModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    OptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    ValueId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletedTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VariantValuesReadModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VariantValuesReadModel_OptionValues_ProductId",
                        column: x => x.ProductId,
                        principalTable: "OptionValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValuesReadModel_Options_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValuesReadModel_ProductOptions_ProductId",
                        column: x => x.ProductId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValuesReadModel_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VariantValuesReadModel_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValuesReadModel_Id",
                table: "VariantValuesReadModel",
                column: "Id",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "ProductId", "ProductVariantId", "ValueId" });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValuesReadModel_ProductId",
                table: "VariantValuesReadModel",
                column: "ProductId",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductVariantId", "ValueId" });

            migrationBuilder.CreateIndex(
                name: "IX_VariantValuesReadModel_ProductVariantId",
                table: "VariantValuesReadModel",
                column: "ProductVariantId",
                unique: true)
                .Annotation("Npgsql:IndexInclude", new[] { "OptionId", "Id", "ProductId", "ValueId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VariantValuesReadModel");
        }
    }
}
