using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_EmployeeBankData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeBankData",
                columns: table => new
                {
                    EmployeeBankDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountFirstName = table.Column<string>(nullable: false),
                    AccountLastName = table.Column<string>(nullable: false),
                    AccountMiddleName = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<string>(maxLength: 11, nullable: false),
                    AccountType = table.Column<string>(nullable: false),
                    BankId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBankData", x => x.EmployeeBankDataId);
                    table.ForeignKey(
                        name: "FK_EmployeeBankData_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeBankData_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBankData_BankId",
                table: "EmployeeBankData",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBankData_EmployeeId",
                table: "EmployeeBankData",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeBankData");
        }
    }
}
