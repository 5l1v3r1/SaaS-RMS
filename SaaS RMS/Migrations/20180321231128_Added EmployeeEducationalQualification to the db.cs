using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedEmployeeEducationalQualificationtothedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeEducationalQualifications",
                columns: table => new
                {
                    EmployeeEducationalQualificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassOfDegree = table.Column<string>(nullable: true),
                    DegreeAttained = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FileUpload = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEducationalQualifications", x => x.EmployeeEducationalQualificationId);
                    table.ForeignKey(
                        name: "FK_EmployeeEducationalQualifications_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducationalQualifications_EmployeeId",
                table: "EmployeeEducationalQualifications",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeEducationalQualifications");
        }
    }
}
