using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class QuickTwo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Restaurants_RestaurantId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EmployeeWorkDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

           

            

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EmployeePersonalDatas",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "EmployeePersonalDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkDatas_RestaurantId",
                table: "EmployeeWorkDatas",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_RestaurantId",
                table: "EmployeePersonalDatas",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePersonalDatas_Restaurants_RestaurantId",
                table: "EmployeePersonalDatas",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Restaurants_RestaurantId",
                table: "Employees",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkDatas_Restaurants_RestaurantId",
                table: "EmployeeWorkDatas",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePersonalDatas_Restaurants_RestaurantId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Restaurants_RestaurantId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkDatas_Restaurants_RestaurantId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeWorkDatas_RestaurantId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePersonalDatas_RestaurantId",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "EmployeePersonalDatas");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "EmployeePersonalDatas");

            migrationBuilder.AlterColumn<int>(
                name: "RestaurantId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Restaurants_RestaurantId",
                table: "Employees",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
