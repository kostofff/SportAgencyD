using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CSS2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClubsApplication",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AthleteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AthleteAdId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubsApplication", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_ClubsApplication_AspNetUsers_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubsApplication_AspNetUsers_ClubId",
                        column: x => x.ClubId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClubsApplication_AthleteAds_AthleteAdId",
                        column: x => x.AthleteAdId,
                        principalTable: "AthleteAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubsApplication_AthleteAdId",
                table: "ClubsApplication",
                column: "AthleteAdId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsApplication_AthleteId",
                table: "ClubsApplication",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClubsApplication_ClubId",
                table: "ClubsApplication",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClubsApplication");
        }
    }
}
