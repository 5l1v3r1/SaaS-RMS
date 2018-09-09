using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Fewchangesinrestaurantmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetUpStatus",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Restaurants");

            migrationBuilder.AddColumn<string>(
                name: "SetUpStatus",
                table: "Restaurants",
                nullable: true);
        }
    }
}
