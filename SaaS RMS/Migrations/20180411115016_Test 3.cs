using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockDetails_Categories_CategoryId",
                table: "StockDetails");

            migrationBuilder.DropIndex(
                name: "IX_StockDetails_CategoryId",
                table: "StockDetails");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "StockDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "StockDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StockDetails_CategoryId",
                table: "StockDetails",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockDetails_Categories_CategoryId",
                table: "StockDetails",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
