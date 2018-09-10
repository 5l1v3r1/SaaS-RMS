using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Changes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "RestaurantSubscriptions");

            migrationBuilder.AlterColumn<int>(
                name: "Duration",
                table: "RestaurantSubscriptions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Amount",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RestaurantSubscriptions");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "RestaurantSubscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "RestaurantSubscriptions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "RestaurantSubscriptions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
