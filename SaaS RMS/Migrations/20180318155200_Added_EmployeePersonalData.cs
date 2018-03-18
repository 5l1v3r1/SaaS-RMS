using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class Added_EmployeePersonalData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeePersonalDatas",
                columns: table => new
                {
                    EmployeePersonalDataId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DOB = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeImage = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    HomePhone = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    LgaId = table.Column<int>(nullable: false),
                    MaritalStatus = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    POB = table.Column<string>(nullable: false),
                    PrimaryAddress = table.Column<string>(nullable: false),
                    SecondaryAddress = table.Column<string>(nullable: true),
                    StateId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    WorkPhone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePersonalDatas", x => x.EmployeePersonalDataId);
                    table.ForeignKey(
                        name: "FK_EmployeePersonalDatas_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePersonalDatas_Lgas_LgaId",
                        column: x => x.LgaId,
                        principalTable: "Lgas",
                        principalColumn: "LgaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_EmployeePersonalDatas_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "StateId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_EmployeeId",
                table: "EmployeePersonalDatas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_LgaId",
                table: "EmployeePersonalDatas",
                column: "LgaId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePersonalDatas_StateId",
                table: "EmployeePersonalDatas",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePersonalDatas");
        }
    }
}
