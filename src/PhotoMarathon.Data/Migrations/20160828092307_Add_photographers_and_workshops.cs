using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PhotoMarathon.Data.Migrations
{
    public partial class Add_photographers_and_workshops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkShop",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShop", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photographers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    IsProfessionist = table.Column<bool>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    RegisterForMarathon = table.Column<bool>(nullable: false),
                    RegisterForWorkShop = table.Column<bool>(nullable: false),
                    WorkshopId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photographers_WorkShop_WorkshopId",
                        column: x => x.WorkshopId,
                        principalTable: "WorkShop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Newsletters",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Newsletters",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_WorkshopId",
                table: "Photographers",
                column: "WorkshopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photographers");

            migrationBuilder.DropTable(
                name: "WorkShop");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Newsletters",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Newsletters",
                nullable: true);
        }
    }
}
