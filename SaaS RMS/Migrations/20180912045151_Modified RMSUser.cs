using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SaaSRMS.Migrations
{
    public partial class ModifiedRMSUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RMSUsers_RMSRoles_RoleId",
                table: "RMSUsers");

            migrationBuilder.DropIndex(
                name: "IX_RMSUsers_RoleId",
                table: "RMSUsers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RMSUsers");

            migrationBuilder.CreateIndex(
                name: "IX_RMSUsers_RMSRoleId",
                table: "RMSUsers",
                column: "RMSRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RMSUsers_RMSRoles_RMSRoleId",
                table: "RMSUsers",
                column: "RMSRoleId",
                principalTable: "RMSRoles",
                principalColumn: "RMSRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RMSUsers_RMSRoles_RMSRoleId",
                table: "RMSUsers");

            migrationBuilder.DropIndex(
                name: "IX_RMSUsers_RMSRoleId",
                table: "RMSUsers");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RMSUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RMSUsers_RoleId",
                table: "RMSUsers",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RMSUsers_RMSRoles_RoleId",
                table: "RMSUsers",
                column: "RoleId",
                principalTable: "RMSRoles",
                principalColumn: "RMSRoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
