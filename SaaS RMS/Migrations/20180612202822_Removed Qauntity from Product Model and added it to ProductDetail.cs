using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class RemovedQauntityfromProductModelandaddedittoProductDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Products");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "ProductDetails",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ProductDetails");

            migrationBuilder.AddColumn<double>(
                name: "Quantity",
                table: "Products",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
