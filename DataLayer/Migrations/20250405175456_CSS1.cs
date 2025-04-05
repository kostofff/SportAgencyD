using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class CSS1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AthletesApplication",
                columns: table => new
                {
                    ApplicationId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AthleteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClubAdId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AthletesApplication", x => x.ApplicationId);
                    table.ForeignKey(
                        name: "FK_AthletesApplication_AspNetUsers_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthletesApplication_AspNetUsers_ClubId",
                        column: x => x.ClubId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AthletesApplication_ClubAds_ClubAdId",
                        column: x => x.ClubAdId,
                        principalTable: "ClubAds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AthletesApplication_AthleteId",
                table: "AthletesApplication",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_AthletesApplication_ClubAdId",
                table: "AthletesApplication",
                column: "ClubAdId");

            migrationBuilder.CreateIndex(
                name: "IX_AthletesApplication_ClubId",
                table: "AthletesApplication",
                column: "ClubId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AthletesApplication");
        }
    }
}
