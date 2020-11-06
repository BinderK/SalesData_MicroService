using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalesData.DATA.Migrations.MsSql
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sale",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleNumber = table.Column<string>(nullable: true),
                    SalesPrice = table.Column<double>(nullable: false),
                    SaleDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sale", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sale");
        }
    }
}
