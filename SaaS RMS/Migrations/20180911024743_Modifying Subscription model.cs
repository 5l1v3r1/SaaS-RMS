using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifyingSubscriptionmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcriptions_Restaurants_RestaurantId",
                table: "Subcriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subcriptions_RestaurantId",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Subcriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Subcriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subcriptions_RestaurantId",
                table: "Subcriptions",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcriptions_Restaurants_RestaurantId",
                table: "Subcriptions",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
