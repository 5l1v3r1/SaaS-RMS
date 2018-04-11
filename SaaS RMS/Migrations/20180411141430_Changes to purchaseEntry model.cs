using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ChangestopurchaseEntrymodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseEntries_Restaurants_RestaurantId",
                table: "PurchaseEntries");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseEntries_RestaurantId",
                table: "PurchaseEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PurchaseEntries_RestaurantId",
                table: "PurchaseEntries",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseEntries_Restaurants_RestaurantId",
                table: "PurchaseEntries",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
