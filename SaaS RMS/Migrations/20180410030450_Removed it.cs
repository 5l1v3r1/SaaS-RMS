using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Removedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondImage",
                table: "LandingInfo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondImage",
                table: "LandingInfo",
                nullable: false,
                defaultValue: "");
        }
    }
}
