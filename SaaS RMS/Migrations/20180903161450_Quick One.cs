using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class QuickOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AppUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AppUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "AppUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "AppUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_RestaurantId",
                table: "AppUsers",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Restaurants_RestaurantId",
                table: "AppUsers",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Restaurants_RestaurantId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_RestaurantId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "AppUsers");
        }
    }
}
