using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_EmployeeMedicalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeMedicalDatas",
                columns: table => new
                {
                    EmployeeMedicalDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BloodGroup = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Genotype = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMedicalDatas", x => x.EmployeeMedicalDataId);
                    table.ForeignKey(
                        name: "FK_EmployeeMedicalDatas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMedicalDatas_EmployeeId",
                table: "EmployeeMedicalDatas",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeMedicalDatas");
        }
    }
}
