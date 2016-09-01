using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class photograp_field_WorkshopId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopId",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers",
                column: "WorkshopId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers");

            migrationBuilder.AlterColumn<int>(
                name: "WorkshopId",
                table: "Photographers",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Photographers_WorkShops_WorkshopId",
                table: "Photographers",
                column: "WorkshopId",
                principalTable: "WorkShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
