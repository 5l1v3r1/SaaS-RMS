using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedEmployeePastWorkExperiencemodeltothedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePastWorkExperiences",
                columns: table => new
                {
                    EmployeePastWorkExperienceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployerContact = table.Column<string>(nullable: false),
                    EmployerLocation = table.Column<string>(nullable: false),
                    EmployerName = table.Column<string>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PositionHeld = table.Column<string>(nullable: false),
                    ReasonForLeaving = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePastWorkExperiences", x => x.EmployeePastWorkExperienceId);
                    table.ForeignKey(
                        name: "FK_EmployeePastWorkExperiences_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePastWorkExperiences_EmployeeId",
                table: "EmployeePastWorkExperiences",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePastWorkExperiences");
        }
    }
}
