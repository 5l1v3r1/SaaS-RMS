using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifiedCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Allegies",
                table: "Customer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BloodGroup",
                table: "Customer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Genotype",
                table: "Customer",
                nullable: false,
                defaultValue: 0);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Allegies",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "BloodGroup",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "Genotype",
                table: "Customer");
        }
    }
}
