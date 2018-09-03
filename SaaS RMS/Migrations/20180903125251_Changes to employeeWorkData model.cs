using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ChangestoemployeeWorkDatamodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Income",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "WorkDays",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkHour",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkMonth",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "WorkWeek",
                table: "EmployeeWorkDatas",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "AppUserAccessKeys",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AppUserAccessKeys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastModified",
                table: "AppUserAccessKeys",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "LastModifiedBy",
                table: "AppUserAccessKeys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "AppUserAccessKeys",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkDatas_DepartmentId",
                table: "EmployeeWorkDatas",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserAccessKeys_RestaurantId",
                table: "AppUserAccessKeys",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserAccessKeys_Restaurants_RestaurantId",
                table: "AppUserAccessKeys",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "RestaurantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkDatas_Departments_DepartmentId",
                table: "EmployeeWorkDatas",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserAccessKeys_Restaurants_RestaurantId",
                table: "AppUserAccessKeys");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkDatas_Departments_DepartmentId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeWorkDatas_DepartmentId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropIndex(
                name: "IX_AppUserAccessKeys_RestaurantId",
                table: "AppUserAccessKeys");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "Income",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "WorkDays",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "WorkHour",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "WorkMonth",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "WorkWeek",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "AppUserAccessKeys");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AppUserAccessKeys");

            migrationBuilder.DropColumn(
                name: "DateLastModified",
                table: "AppUserAccessKeys");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "AppUserAccessKeys");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "AppUserAccessKeys");
        }
    }
}
