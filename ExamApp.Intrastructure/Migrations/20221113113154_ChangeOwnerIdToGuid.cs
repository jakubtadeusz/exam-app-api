using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamApp.Intrastructure.Migrations
{
    public partial class ChangeOwnerIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Exams");
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Exams",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Examined");
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Examined",
                type: "uniqueidentifier",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Exams",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Examined",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
