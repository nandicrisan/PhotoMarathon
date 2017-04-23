using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class cms_subtitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subtitle",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Subtitle",
                table: "Articles");
        }
    }
}
