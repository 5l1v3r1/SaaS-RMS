using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedProductDetailandremovedStockDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_StockDetails_StockDetailId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "StockDetails");

            migrationBuilder.RenameColumn(
                name: "StockDetailId",
                table: "Purchases",
                newName: "ProductDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_StockDetailId",
                table: "Purchases",
                newName: "IX_Purchases_ProductDetailId");

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Meaurement = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailId);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductId",
                table: "ProductDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_RestaurantId",
                table: "ProductDetails",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_VendorId",
                table: "ProductDetails",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_ProductDetails_ProductDetailId",
                table: "Purchases",
                column: "ProductDetailId",
                principalTable: "ProductDetails",
                principalColumn: "ProductDetailId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_ProductDetails_ProductDetailId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.RenameColumn(
                name: "ProductDetailId",
                table: "Purchases",
                newName: "StockDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ProductDetailId",
                table: "Purchases",
                newName: "IX_Purchases_StockDetailId");

            migrationBuilder.CreateTable(
                name: "StockDetails",
                columns: table => new
                {
                    StockDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    Meaurement = table.Column<string>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false),
                    VendorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDetails", x => x.StockDetailId);
                    table.ForeignKey(
                        name: "FK_StockDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetails_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockDetails_Vendors_VendorId",
                        column: x => x.VendorId,
                        principalTable: "Vendors",
                        principalColumn: "VendorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_ProductId",
                table: "StockDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_RestaurantId",
                table: "StockDetails",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_VendorId",
                table: "StockDetails",
                column: "VendorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_StockDetails_StockDetailId",
                table: "Purchases",
                column: "StockDetailId",
                principalTable: "StockDetails",
                principalColumn: "StockDetailId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
