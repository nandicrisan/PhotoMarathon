using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class blog_change_datetime_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BlogItems",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateAdded",
                table: "BlogItems",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "BlogItems",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "BlogItems",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DateAdded",
                table: "BlogItems",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "BlogItems",
                nullable: true);
        }
    }
}
