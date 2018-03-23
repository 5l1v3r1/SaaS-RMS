using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedRestaurantQualificationtothedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantQualificationId",
                table: "EmployeeEducationalQualifications",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RestaurantQualifications",
                columns: table => new
                {
                    RestaurantQualificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RestaurantId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantQualifications", x => x.RestaurantQualificationId);
                    table.ForeignKey(
                        name: "FK_RestaurantQualifications_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducationalQualifications_RestaurantQualificationId",
                table: "EmployeeEducationalQualifications",
                column: "RestaurantQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantQualifications_RestaurantId",
                table: "RestaurantQualifications",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducationalQualifications_RestaurantQualifications_RestaurantQualificationId",
                table: "EmployeeEducationalQualifications",
                column: "RestaurantQualificationId",
                principalTable: "RestaurantQualifications",
                principalColumn: "RestaurantQualificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducationalQualifications_RestaurantQualifications_RestaurantQualificationId",
                table: "EmployeeEducationalQualifications");

            migrationBuilder.DropTable(
                name: "RestaurantQualifications");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducationalQualifications_RestaurantQualificationId",
                table: "EmployeeEducationalQualifications");

            migrationBuilder.DropColumn(
                name: "RestaurantQualificationId",
                table: "EmployeeEducationalQualifications");
        }
    }
}
