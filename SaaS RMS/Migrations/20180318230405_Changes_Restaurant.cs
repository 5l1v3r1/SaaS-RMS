using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Changes_Restaurant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_States_StateId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Restaurants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LgaId",
                table: "Restaurants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants",
                column: "LgaId",
                principalTable: "Lgas",
                principalColumn: "LgaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_States_StateId",
                table: "Restaurants",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_States_StateId",
                table: "Restaurants");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "Restaurants",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LgaId",
                table: "Restaurants",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_Lgas_LgaId",
                table: "Restaurants",
                column: "LgaId",
                principalTable: "Lgas",
                principalColumn: "LgaId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_States_StateId",
                table: "Restaurants",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
