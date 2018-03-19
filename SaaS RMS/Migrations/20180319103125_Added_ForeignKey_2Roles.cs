using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_ForeignKey_2Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Roles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RestaurantId",
                table: "Roles",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Restaurants_RestaurantId",
                table: "Roles",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Restaurants_RestaurantId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RestaurantId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Roles");
        }
    }
}
