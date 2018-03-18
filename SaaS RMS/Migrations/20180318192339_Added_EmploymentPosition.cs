using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_EmploymentPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmploymentPositionId",
                table: "EmployeeWorkDatas",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmploymentPositions",
                columns: table => new
                {
                    EmploymentPositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    Income = table.Column<long>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false),
                    SeniorMember = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentPositions", x => x.EmploymentPositionId);
                    table.ForeignKey(
                        name: "FK_EmploymentPositions_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploymentPositions_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkDatas_EmploymentPositionId",
                table: "EmployeeWorkDatas",
                column: "EmploymentPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentPositions_EmployeeId",
                table: "EmploymentPositions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentPositions_RestaurantId",
                table: "EmploymentPositions",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeWorkDatas_EmploymentPositions_EmploymentPositionId",
                table: "EmployeeWorkDatas",
                column: "EmploymentPositionId",
                principalTable: "EmploymentPositions",
                principalColumn: "EmploymentPositionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeWorkDatas_EmploymentPositions_EmploymentPositionId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropTable(
                name: "EmploymentPositions");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeWorkDatas_EmploymentPositionId",
                table: "EmployeeWorkDatas");

            migrationBuilder.DropColumn(
                name: "EmploymentPositionId",
                table: "EmployeeWorkDatas");
        }
    }
}
