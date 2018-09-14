using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifiedPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Packages",
                newName: "Description1");

            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Packages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description3",
                table: "Packages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "Description3",
                table: "Packages");

            migrationBuilder.RenameColumn(
                name: "Description1",
                table: "Packages",
                newName: "Description");
        }
    }
}
