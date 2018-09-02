using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class AddedAppUserKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserAccessKeys",
                columns: table => new
                {
                    AppUserAccessKeyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountActivationAccessCode = table.Column<string>(nullable: true),
                    AppUserId = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: true),
                    PasswordAccessCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserAccessKeys", x => x.AppUserAccessKeyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserAccessKeys");
        }
    }
}
