using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Kursova.DatabaseContext.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OffersState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OffersState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TenderState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Patronimyc = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "varchar", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tender",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(20,2)", precision: 20, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 512, nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExecuterId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tender_TenderState_StateId",
                        column: x => x.StateId,
                        principalTable: "TenderState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tender_User_ExecuterId",
                        column: x => x.ExecuterId,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tender_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    OffererId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(20,2)", precision: 20, scale: 2, nullable: false),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 512, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_OffersState_StateId",
                        column: x => x.StateId,
                        principalTable: "OffersState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_Tender_TenderId",
                        column: x => x.TenderId,
                        principalTable: "Tender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offer_User_OffererId",
                        column: x => x.OffererId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offer_OffererId",
                table: "Offer",
                column: "OffererId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_StateId",
                table: "Offer",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_TenderId",
                table: "Offer",
                column: "TenderId");

            migrationBuilder.CreateIndex(
                name: "IX_OffersState_Name",
                table: "OffersState",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tender_ExecuterId",
                table: "Tender",
                column: "ExecuterId");

            migrationBuilder.CreateIndex(
                name: "IX_Tender_OwnerId",
                table: "Tender",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tender_StateId",
                table: "Tender",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_TenderState_Name",
                table: "TenderState",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "OffersState");

            migrationBuilder.DropTable(
                name: "Tender");

            migrationBuilder.DropTable(
                name: "TenderState");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
