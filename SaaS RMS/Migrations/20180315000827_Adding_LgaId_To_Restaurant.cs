using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Adding_LgaId_To_Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LgaId",
                table: "Restaurants",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_LgaId",
                table: "Restaurants",
                column: "LgaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants",
                column: "LgaId",
                principalTable: "Lgas",
                principalColumn: "LgaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_LgaId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "LgaId",
                table: "Restaurants");
        }
    }
}
