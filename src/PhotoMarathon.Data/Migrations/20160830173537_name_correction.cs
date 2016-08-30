using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class name_correction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_WorkShop_WorkshopId",
                table: "Photographers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkShop",
                table: "WorkShop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkShops",
                table: "WorkShop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers",
                column: "WorkshopId",
                principalTable: "WorkShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "WorkShop",
                newName: "WorkShops");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WorkShops",
                table: "WorkShops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WorkShop",
                table: "WorkShops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_WorkShop_WorkshopId",
                table: "Photographers",
                column: "WorkshopId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameTable(
                name: "WorkShops",
                newName: "WorkShop");
        }
    }
}
