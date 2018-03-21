using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedrestaurantIdasaforeignkeytobank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Banks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Banks_RestaurantId",
                table: "Banks",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Banks_Restaurants_RestaurantId",
                table: "Banks",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Banks_Restaurants_RestaurantId",
                table: "Banks");

            migrationBuilder.DropIndex(
                name: "IX_Banks_RestaurantId",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Banks");
        }
    }
}
