using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class RemovedPurchasesandaddedPurhcaseOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    PurchaseOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Payment = table.Column<double>(nullable: false),
                    ProductDetailId = table.Column<int>(nullable: false),
                    PurchaseEntryId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.PurchaseOrderId);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_OrderEntries_PurchaseEntryId",
                        column: x => x.PurchaseEntryId,
                        principalTable: "OrderEntries",
                        principalColumn: "OrderEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ProductDetailId",
                table: "PurchaseOrders",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_PurchaseEntryId",
                table: "PurchaseOrders",
                column: "PurchaseEntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    Mode = table.Column<int>(nullable: false),
                    Payment = table.Column<double>(nullable: false),
                    ProductDetailId = table.Column<int>(nullable: false),
                    PurchaseEntryId = table.Column<int>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_Purchases_ProductDetails_ProductDetailId",
                        column: x => x.ProductDetailId,
                        principalTable: "ProductDetails",
                        principalColumn: "ProductDetailId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchases_OrderEntries_PurchaseEntryId",
                        column: x => x.PurchaseEntryId,
                        principalTable: "OrderEntries",
                        principalColumn: "OrderEntryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductDetailId",
                table: "Purchases",
                column: "ProductDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_PurchaseEntryId",
                table: "Purchases",
                column: "PurchaseEntryId");
        }
    }
}
