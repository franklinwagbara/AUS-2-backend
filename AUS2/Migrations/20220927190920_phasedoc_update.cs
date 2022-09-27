using Microsoft.EntityFrameworkCore.Migrations;

namespace AUS2.Migrations
{
    public partial class phasedoc_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhaseDocuments_Categories_CategoryId",
                table: "PhaseDocuments");

            migrationBuilder.DropIndex(
                name: "IX_PhaseDocuments_CategoryId",
                table: "PhaseDocuments");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "PhaseDocuments");

            migrationBuilder.DropColumn(
                name: "CatoeryId",
                table: "PhaseDocuments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "PhaseDocuments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CatoeryId",
                table: "PhaseDocuments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhaseDocuments_CategoryId",
                table: "PhaseDocuments",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhaseDocuments_Categories_CategoryId",
                table: "PhaseDocuments",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
