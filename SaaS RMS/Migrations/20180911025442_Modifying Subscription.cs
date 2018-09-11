using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifyingSubscription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "RestaurantSubscriptions");

            migrationBuilder.AddColumn<string>(
                name: "EndDate",
                table: "RestaurantSubscriptions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartDate",
                table: "RestaurantSubscriptions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RestaurantSubscriptions");

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
