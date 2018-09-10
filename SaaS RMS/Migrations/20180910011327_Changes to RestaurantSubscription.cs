using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ChangestoRestaurantSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "RestaurantSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "RestaurantSubscriptions",
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
                name: "CreatedBy",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "RestaurantSubscriptions");
        }
    }
}
