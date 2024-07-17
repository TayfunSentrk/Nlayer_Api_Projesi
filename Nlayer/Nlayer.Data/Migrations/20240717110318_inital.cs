using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nlayer.Data.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductFeatures_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(8857), "Kalemler", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(8867), "Kitaplar", null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Name", "UpdatedDate" },
                values: new object[] { 3, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(8868), "Defterler", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Name", "Price", "Stock", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(9295), "Kalem1", 100m, 20, null },
                    { 2, 1, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(9298), "Kalem2", 200m, 20, null },
                    { 3, 1, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(9299), "Kalem3", 5000m, 20, null },
                    { 4, 2, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(9300), "kitap1", 5000m, 20, null },
                    { 5, 2, new DateTime(2024, 7, 17, 14, 3, 18, 708, DateTimeKind.Local).AddTicks(9301), "kitap2", 5000m, 20, null }
                });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "Id", "Color", "Height", "ProductId", "Weight" },
                values: new object[] { 1, "kirmizi", 200, 1, 100 });

            migrationBuilder.InsertData(
                table: "ProductFeatures",
                columns: new[] { "Id", "Color", "Height", "ProductId", "Weight" },
                values: new object[] { 2, "mavi", 200, 2, 300 });

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_ProductId",
                table: "ProductFeatures",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductFeatures");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
