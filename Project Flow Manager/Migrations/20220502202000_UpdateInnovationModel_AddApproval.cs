using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project_Flow_Manager.Migrations
{
    public partial class UpdateInnovationModel_AddApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubmittedBy",
                table: "Innovation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalId",
                table: "Innovation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Approval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Outcome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApprovedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Approval", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Innovation_ApprovalId",
                table: "Innovation",
                column: "ApprovalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation",
                column: "ApprovalId",
                principalTable: "Approval",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Innovation_Approval_ApprovalId",
                table: "Innovation");

            migrationBuilder.DropTable(
                name: "Approval");

            migrationBuilder.DropIndex(
                name: "IX_Innovation_ApprovalId",
                table: "Innovation");

            migrationBuilder.DropColumn(
                name: "ApprovalId",
                table: "Innovation");

            migrationBuilder.AlterColumn<string>(
                name: "SubmittedBy",
                table: "Innovation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
