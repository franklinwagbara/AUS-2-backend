using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AUS2.Migrations
{
    public partial class fiedofficeupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "FieldOffices");

            migrationBuilder.DropColumn(
                name: "FieldType",
                table: "FieldOffices");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "FieldOffices",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "FieldOffices",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FieldOffices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "FieldOffices");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "FieldOffices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "FieldOffices",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "FieldOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FieldType",
                table: "FieldOffices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
