using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddSubscriptionmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriprionStartDate",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "SubscriptionDuration",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "SubscriptonEndDate",
                table: "Restaurants");

            migrationBuilder.CreateTable(
                name: "RestaurantSubscriptions",
                columns: table => new
                {
                    RestaurantSubscriptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Duration = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    PackageId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantSubscriptions", x => x.RestaurantSubscriptionId);
                    table.ForeignKey(
                        name: "FK_RestaurantSubscriptions_Packages_PackageId",
                        column: x => x.PackageId,
                        principalTable: "Packages",
                        principalColumn: "PackageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantSubscriptions_PackageId",
                table: "RestaurantSubscriptions",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantSubscriptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriprionStartDate",
                table: "Restaurants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SubscriptionDuration",
                table: "Restaurants",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptonEndDate",
                table: "Restaurants",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
