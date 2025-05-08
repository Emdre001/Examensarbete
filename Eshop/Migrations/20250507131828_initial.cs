using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eshop.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    ColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.ColorId);
                });

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    SizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SizeValue = table.Column<int>(type: "int", nullable: false),
                    SizeStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.SizeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPhoneNr = table.Column<int>(type: "int", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbBrandBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<int>(type: "int", nullable: false),
                    ProductRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Brands_DbBrandBrandId",
                        column: x => x.DbBrandBrandId,
                        principalTable: "Brands",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbUserUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderAmount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Users_DbUserUserId",
                        column: x => x.DbUserUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbColorDbProduct",
                columns: table => new
                {
                    DbColorsColorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbProductsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbColorDbProduct", x => new { x.DbColorsColorId, x.DbProductsProductId });
                    table.ForeignKey(
                        name: "FK_DbColorDbProduct_Colors_DbColorsColorId",
                        column: x => x.DbColorsColorId,
                        principalTable: "Colors",
                        principalColumn: "ColorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbColorDbProduct_Products_DbProductsProductId",
                        column: x => x.DbProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbProductDbSize",
                columns: table => new
                {
                    DbProductsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbSizesSizeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbProductDbSize", x => new { x.DbProductsProductId, x.DbSizesSizeId });
                    table.ForeignKey(
                        name: "FK_DbProductDbSize_Products_DbProductsProductId",
                        column: x => x.DbProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbProductDbSize_Sizes_DbSizesSizeId",
                        column: x => x.DbSizesSizeId,
                        principalTable: "Sizes",
                        principalColumn: "SizeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DbOrderDbProduct",
                columns: table => new
                {
                    DbOrdersOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DbProductsProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbOrderDbProduct", x => new { x.DbOrdersOrderId, x.DbProductsProductId });
                    table.ForeignKey(
                        name: "FK_DbOrderDbProduct_Orders_DbOrdersOrderId",
                        column: x => x.DbOrdersOrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DbOrderDbProduct_Products_DbProductsProductId",
                        column: x => x.DbProductsProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbColorDbProduct_DbProductsProductId",
                table: "DbColorDbProduct",
                column: "DbProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DbOrderDbProduct_DbProductsProductId",
                table: "DbOrderDbProduct",
                column: "DbProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DbProductDbSize_DbSizesSizeId",
                table: "DbProductDbSize",
                column: "DbSizesSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DbUserUserId",
                table: "Orders",
                column: "DbUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DbBrandBrandId",
                table: "Products",
                column: "DbBrandBrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbColorDbProduct");

            migrationBuilder.DropTable(
                name: "DbOrderDbProduct");

            migrationBuilder.DropTable(
                name: "DbProductDbSize");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
