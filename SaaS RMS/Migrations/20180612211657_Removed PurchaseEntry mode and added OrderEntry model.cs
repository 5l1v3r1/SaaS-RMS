using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class RemovedPurchaseEntrymodeandaddedOrderEntrymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_PurchaseEntries_PurchaseEntryId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "PurchaseEntries");

            migrationBuilder.CreateTable(
                name: "OrderEntries",
                columns: table => new
                {
                    OrderEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderEntries", x => x.OrderEntryId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_OrderEntries_PurchaseEntryId",
                table: "Purchases",
                column: "PurchaseEntryId",
                principalTable: "OrderEntries",
                principalColumn: "OrderEntryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_OrderEntries_PurchaseEntryId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "OrderEntries");

            migrationBuilder.CreateTable(
                name: "PurchaseEntries",
                columns: table => new
                {
                    PurchaseEntryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseEntries", x => x.PurchaseEntryId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_PurchaseEntries_PurchaseEntryId",
                table: "Purchases",
                column: "PurchaseEntryId",
                principalTable: "PurchaseEntries",
                principalColumn: "PurchaseEntryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
