using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductManagement.Api.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCategory",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Description = table.Column<string>(type: "VARCHAR(550)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategory", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "TblProduct",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "VARCHAR(50)", nullable: true),
                    Cost = table.Column<decimal>(type: "DECIMAL(24,2)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(550)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProduct", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "TblCategoryMapping",
                columns: table => new
                {
                    CategoryMappingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategoryMapping", x => x.CategoryMappingID);
                    table.ForeignKey(
                        name: "FK_TblCategoryMapping_TblCategory_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "TblCategory",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblCategoryMapping_TblProduct_ProductID",
                        column: x => x.ProductID,
                        principalTable: "TblProduct",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryMapping_CategoryID",
                table: "TblCategoryMapping",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryMapping_ProductID",
                table: "TblCategoryMapping",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblCategoryMapping");

            migrationBuilder.DropTable(
                name: "TblCategory");

            migrationBuilder.DropTable(
                name: "TblProduct");
        }
    }
}
