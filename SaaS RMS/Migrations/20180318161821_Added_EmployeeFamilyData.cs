using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_EmployeeFamilyData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBankData_Banks_BankId",
                table: "EmployeeBankData");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBankData_Employees_EmployeeId",
                table: "EmployeeBankData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeBankData",
                table: "EmployeeBankData");

            migrationBuilder.RenameTable(
                name: "EmployeeBankData",
                newName: "EmployeeBankDatas");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBankData_EmployeeId",
                table: "EmployeeBankDatas",
                newName: "IX_EmployeeBankDatas_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBankData_BankId",
                table: "EmployeeBankDatas",
                newName: "IX_EmployeeBankDatas_BankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeBankDatas",
                table: "EmployeeBankDatas",
                column: "EmployeeBankDataId");

            migrationBuilder.CreateTable(
                name: "EmployeeFamilyDatas",
                columns: table => new
                {
                    EmployeeFamilyDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    NextOfKin = table.Column<string>(maxLength: 10, nullable: false),
                    Relationship = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeFamilyDatas", x => x.EmployeeFamilyDataId);
                    table.ForeignKey(
                        name: "FK_EmployeeFamilyDatas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeFamilyDatas_EmployeeId",
                table: "EmployeeFamilyDatas",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBankDatas_Banks_BankId",
                table: "EmployeeBankDatas",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBankDatas_Employees_EmployeeId",
                table: "EmployeeBankDatas",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBankDatas_Banks_BankId",
                table: "EmployeeBankDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeBankDatas_Employees_EmployeeId",
                table: "EmployeeBankDatas");

            migrationBuilder.DropTable(
                name: "EmployeeFamilyDatas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeBankDatas",
                table: "EmployeeBankDatas");

            migrationBuilder.RenameTable(
                name: "EmployeeBankDatas",
                newName: "EmployeeBankData");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBankDatas_EmployeeId",
                table: "EmployeeBankData",
                newName: "IX_EmployeeBankData_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeBankDatas_BankId",
                table: "EmployeeBankData",
                newName: "IX_EmployeeBankData_BankId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeBankData",
                table: "EmployeeBankData",
                column: "EmployeeBankDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBankData_Banks_BankId",
                table: "EmployeeBankData",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeBankData_Employees_EmployeeId",
                table: "EmployeeBankData",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
