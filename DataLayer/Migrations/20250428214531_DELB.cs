using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class DELB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthletesApplication_AspNetUsers_AthleteId",
                table: "AthletesApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubsApplication_AspNetUsers_ClubId",
                table: "ClubsApplication");

            migrationBuilder.AddForeignKey(
                name: "FK_AthletesApplication_AspNetUsers_AthleteId",
                table: "AthletesApplication",
                column: "AthleteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsApplication_AspNetUsers_ClubId",
                table: "ClubsApplication",
                column: "ClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthletesApplication_AspNetUsers_AthleteId",
                table: "AthletesApplication");

            migrationBuilder.DropForeignKey(
                name: "FK_ClubsApplication_AspNetUsers_ClubId",
                table: "ClubsApplication");

            migrationBuilder.AddForeignKey(
                name: "FK_AthletesApplication_AspNetUsers_AthleteId",
                table: "AthletesApplication",
                column: "AthleteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClubsApplication_AspNetUsers_ClubId",
                table: "ClubsApplication",
                column: "ClubId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
