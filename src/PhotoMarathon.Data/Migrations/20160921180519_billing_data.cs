using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoMarathon.Data.Migrations
{
    public partial class billing_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingDatas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    Cnp = table.Column<string>(nullable: true),
                    Cont = table.Column<string>(nullable: true),
                    LegalPerson = table.Column<bool>(nullable: false),
                    NrReg = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingDatas_Photographers_Id",
                        column: x => x.Id,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillingDatas_Id",
                table: "BillingDatas",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingDatas");
        }
    }
}
