using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedTransporttoAccessLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AccessLogs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AccessLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "AccessLogs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "AccessLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "AccessLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AccessLogs_RestaurantId",
                table: "AccessLogs",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessLogs_Restaurants_RestaurantId",
                table: "AccessLogs",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessLogs_Restaurants_RestaurantId",
                table: "AccessLogs");

            migrationBuilder.DropIndex(
                name: "IX_AccessLogs_RestaurantId",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AccessLogs");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "AccessLogs");
        }
    }
}
