using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class name_for_cms_page : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sections",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Pages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Articles");
        }
    }
}
