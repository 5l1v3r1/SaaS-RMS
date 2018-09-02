using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Madefewchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePersonalDatas_Lgas_LgaId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePersonalDatas_States_StateId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePersonalDatas_LgaId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePersonalDatas_StateId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "LgaId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "EmployeePersonalDatas");

            migrationBuilder.AddColumn<string>(
                name: "LGA",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LGA",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "State",
                table: "EmployeePersonalDatas");

            migrationBuilder.AddColumn<int>(
                name: "LgaId",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_LgaId",
                table: "EmployeePersonalDatas",
                column: "LgaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_StateId",
                table: "EmployeePersonalDatas",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePersonalDatas_Lgas_LgaId",
                table: "EmployeePersonalDatas",
                column: "LgaId",
                principalTable: "Lgas",
                principalColumn: "LgaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePersonalDatas_States_StateId",
                table: "EmployeePersonalDatas",
                column: "StateId",
                principalTable: "States",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
