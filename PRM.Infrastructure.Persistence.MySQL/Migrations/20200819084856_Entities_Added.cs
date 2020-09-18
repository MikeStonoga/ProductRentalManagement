using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PRM.Infrastructure.Persistence.MySQL.Migrations
{
    public partial class Entities_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    RentDailyPrice = table.Column<decimal>(nullable: false),
                    RentDailyLateFee = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "renters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PersonImage = table.Column<byte[]>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    GovernmentRegistrationDocumentCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_renters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RenterId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    RentPeriod_StartDate = table.Column<DateTime>(nullable: true),
                    RentPeriod_EndDate = table.Column<DateTime>(nullable: true),
                    DailyPrice = table.Column<decimal>(nullable: false),
                    DailyLateFee = table.Column<decimal>(nullable: false),
                    WasProductDamaged = table.Column<bool>(nullable: false),
                    DamageFee = table.Column<decimal>(nullable: false),
                    Discount = table.Column<decimal>(nullable: false),
                    RentedProductsCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "products_rental_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RentId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products_rental_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_products_rental_history_products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_products_rental_history_rents_RentId",
                        column: x => x.RentId,
                        principalTable: "rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "renters_rental_history",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CreatorId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    LastModifierId = table.Column<Guid>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    DeleterId = table.Column<Guid>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RentId = table.Column<Guid>(nullable: false),
                    RenterId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_renters_rental_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_renters_rental_history_rents_RentId",
                        column: x => x.RentId,
                        principalTable: "rents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_renters_rental_history_renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_products_rental_history_ProductId",
                table: "products_rental_history",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_products_rental_history_RentId",
                table: "products_rental_history",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_renters_GovernmentRegistrationDocumentCode",
                table: "renters",
                column: "GovernmentRegistrationDocumentCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_renters_rental_history_RentId",
                table: "renters_rental_history",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_renters_rental_history_RenterId",
                table: "renters_rental_history",
                column: "RenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "products_rental_history");

            migrationBuilder.DropTable(
                name: "renters_rental_history");

            migrationBuilder.DropTable(
                name: "products");

            migrationBuilder.DropTable(
                name: "rents");

            migrationBuilder.DropTable(
                name: "renters");
        }
    }
}
