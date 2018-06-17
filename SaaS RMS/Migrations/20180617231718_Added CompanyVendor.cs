using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedCompanyVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VendorItem",
                table: "Vendors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CompanyVendors",
                columns: table => new
                {
                    CompanyVendorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    OfficeNumber = table.Column<string>(nullable: true),
                    Request = table.Column<string>(nullable: false),
                    VendorItem = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyVendors", x => x.CompanyVendorId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyVendors");

            migrationBuilder.DropColumn(
                name: "VendorItem",
                table: "Vendors");
        }
    }
}
