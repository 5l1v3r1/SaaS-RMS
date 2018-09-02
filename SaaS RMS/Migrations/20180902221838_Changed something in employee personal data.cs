using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Changedsomethinginemployeepersonaldata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DOB",
                table: "EmployeePersonalDatas",
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DOB",
                table: "EmployeePersonalDatas",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
