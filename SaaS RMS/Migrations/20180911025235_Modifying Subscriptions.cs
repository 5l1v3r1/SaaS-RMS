using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifyingSubscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Subcriptions");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Subcriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantSubscriptions_RestaurantId",
                table: "RestaurantSubscriptions",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantSubscriptions_Restaurants_RestaurantId",
                table: "RestaurantSubscriptions",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantSubscriptions_Restaurants_RestaurantId",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantSubscriptions_RestaurantId",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Subcriptions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "RestaurantSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "Subcriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "Subcriptions",
                nullable: true);
        }
    }
}
