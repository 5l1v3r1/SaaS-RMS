using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaS_RMS.Data.Migrations
{
    public partial class Added_Restaurant_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    RestaurantId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessCode = table.Column<string>(nullable: true),
                    ContactEmail = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    LGA = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    Motto = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: true),
                    SetUpStatus = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: false),
                    SubscriprionStartDate = table.Column<DateTime>(nullable: false),
                    SubscriptionDuration = table.Column<string>(nullable: true),
                    SubscriptonEndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.RestaurantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
