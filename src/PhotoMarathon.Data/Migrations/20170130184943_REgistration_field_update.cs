using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class REgistration_field_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Photographers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Photographers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EditionId",
                table: "Photographers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Photographers");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Photographers");
        }
    }
}
